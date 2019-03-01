import { Injectable } from '@angular/core';

import { Component, Inject } from '@angular/core';
import { OrderService } from './order.service';

async function sleep(ms: number) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

async function processOrderPromise(command: ProcessOrderCommand) {
  return new Promise(resolve => resolve(command));
}

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
  providers: [OrderService]
})

export class OrderComponent {
  public awaitingOrders$: Orders[];
  public finishedOrders$: Orders[];
  public threads$: OrderThreads[];
  public selectedThread: OrderThreads;
  public selectedOrder: Orders;

  constructor(private orderService: OrderService) { }

  ngOnInit() {
    this.getOrders();
    this.getThreads();
  }

  onSelectThread(thread: OrderThreads): void {
    this.selectedThread = thread;
  }

  onSelectOrder(order: Orders): void {
    this.selectedOrder = order;
  }

  getOrders() {
    this.orderService.getOrders()
      .subscribe(orders => this.awaitingOrders$ = orders.filter(
        order => order.state === OrderState.Waiting));

    this.orderService.getOrders()
      .subscribe(orders => this.finishedOrders$ = orders.filter(
        order => order.state === OrderState.Finished));
  }

  getThreads() {
    this.orderService.getThreads()
      .subscribe(threads => this.threads$ = threads);
  }

  addOrder() {
    var command: AddOrderCommand = { state: OrderState.Waiting };
    this.orderService.addOrder(command)
      .subscribe(order => this.awaitingOrders$.push(order));
  }

  addThread() {
    var command: AddThreadCommand = { state: OrderThreadState.Stopped };
    this.orderService.addThread(command)
      .subscribe(thread => this.threads$.push(thread));
  }

  onIntervalChange(newInterval: number) {
    var command: UpdateThreadIntervalCommand = {
      interval: newInterval,
      threadId: this.selectedThread.id
    };

    this.orderService.updateThreadInterval(command)
      .subscribe(thread => this.selectedThread.interval = thread.interval);
  }

  changeThreadState() {
    switch (this.selectedThread.state) {
      case (OrderThreadState.Stopped):
        this.selectedThread.state = OrderThreadState.Working;
        this.startThread();
        break;
      case (OrderThreadState.Working):
        this.selectedThread.state = OrderThreadState.Stopped;
        break;
      default:
        break;
    }
  }

  startThread() {
    var command: ChangeThreadStateCommand = {
      id: this.selectedThread.id,
      interval: this.selectedThread.interval,
      state: this.selectedThread.state
    };

    (async () => {
      var currentState = command.state;
      while (currentState === OrderThreadState.Working) {

        var order = this.awaitingOrders$[0];
        this.awaitingOrders$ = this.awaitingOrders$.filter(o => o !== order);

        if (order !== null && order !== undefined) {

          var processCommand: ProcessOrderCommand = {
            threadId: command.id,
            orderId: order.id
          };

          await this.orderService.processOrder(processCommand)
            .subscribe(order => {
              if (order !== null) {
                this.finishedOrders$.push(order);
              }
            });
        }

        var interval = this.threads$.find(thread => thread.id === command.id).interval;
        await sleep(interval);

        currentState = this.threads$.find(thread => thread.id === command.id).state;
        if (currentState === OrderThreadState.Stopped) {
          break;
        }
      }
    })();
  }
}

export interface Orders {
  id: number;
  creationDate: Date;
  state: OrderState;
  threadId: number;
}

export interface OrderThreads {
  id: number;
  interval: number;
  state: OrderThreadState;
}

export enum OrderState {
  Waiting, Working, Finished
}

export enum OrderThreadState {
  Working, Stopped
}

export interface AddOrderCommand {
  state: OrderState
}

export interface UpdateThreadIntervalCommand {
  interval: number;
  threadId: number;
}

export interface AddThreadCommand {
  state: OrderThreadState
}

export interface ChangeThreadStateCommand {
  id: number;
  interval: number;
  state: OrderThreadState;
}

export interface ProcessOrderCommand {
  threadId: number;
  orderId: number;
}
