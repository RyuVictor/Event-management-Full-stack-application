// import { Component, Input, OnChanges, SimpleChanges, OnDestroy } from '@angular/core';

import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { Event } from '../../models/event';

import { BajajEventsService } from '../../services/bajaj-events.service';


@Component({
  selector: 'bajaj-event-details',
  templateUrl: './event-details.component.html',
  styleUrl: './event-details.component.css',
})
// export class EventDetailsComponent implements OnChanges, OnDestroy {
  export class EventDetailsComponent implements OnInit, OnDestroy {
  constructor(private _eventsService: BajajEventsService, private _activatedRoute : ActivatedRoute) {}

  private _eventServiceSubscription: Subscription;
  // ngOnChanges(changes: SimpleChanges): void {
  //   this._eventServiceSubscription = this._eventsService
  //     .getEventDetails(this.eventId)
  //     .subscribe({
  //       next: (data) => (this.event = data),
  //       error: (err) => console.log(err),
  //     });
  // }
  ngOnInit(): void {
    let eventId = this._activatedRoute.snapshot.params['eventId'] as number;
      this._eventServiceSubscription = this._eventsService
      .getEventDetails(eventId)
      .subscribe({
        next: (data) => (this.event = data),
        error: (err) => console.log(err),
      });
  }
  ngOnDestroy(): void {
    if (this._eventServiceSubscription)
      this._eventServiceSubscription.unsubscribe();
  }
  title: string = 'Details of - ';
  // @Input() eventId: number;
  event: Event;
}
