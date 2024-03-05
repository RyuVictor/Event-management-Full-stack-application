import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {Event} from "../models/event";

@Injectable()
export class BajajEventsService {
  constructor(private _httpClient: HttpClient) {}
  private _serviceBaseUrl: string = 'https://localhost:7081/api';
  getAllEvents(): Observable<Event[]> {
    return this._httpClient.get<Event[]>(`${this._serviceBaseUrl}/events`);
  }
  getEventDetails(eventId : number): Observable<Event> { 
    return this._httpClient.get<Event>(`${this._serviceBaseUrl}/events/${eventId}`);
  }
  regiserEvent(event:Event) : Observable<Event>{
    event.logo = "images/noimage.png"
    return this._httpClient.post<Event>(`${this._serviceBaseUrl}/events`,event);
  }
}