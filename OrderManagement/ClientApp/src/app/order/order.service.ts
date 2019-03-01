import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { Orders, OrderThreads, AddOrderCommand, UpdateThreadIntervalCommand, AddThreadCommand, ProcessOrderCommand } from './order.component';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject } from '@angular/core';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable()
export class OrderService {
  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getOrders(): Observable<Orders[]> {
    return this.http.get<Orders[]>(this.baseUrl + 'api/Order/GetOrders');
  }

  getThreads(): Observable<OrderThreads[]> {
    return this.http.get<OrderThreads[]>(this.baseUrl + 'api/Order/GetThreads');
  }

  addOrder(command: AddOrderCommand): Observable<Orders> {
    return this.http.post<Orders>(this.baseUrl + 'api/Order/AddOrder', command);
  }

  updateThreadInterval(command: UpdateThreadIntervalCommand): Observable<OrderThreads> {
    return this.http.post<OrderThreads>(this.baseUrl + 'api/Order/UpdateThreadInterval', command);
  }

  addThread(command: AddThreadCommand): Observable<OrderThreads> {
    return this.http.post<OrderThreads>(this.baseUrl + 'api/Order/AddThread', command);
  }

  processOrder(command: ProcessOrderCommand): Observable<Orders> {
    return this.http.post<Orders>(this.baseUrl + 'api/Order/ProcessOrder', command);
  }
}
