import { Component, OnInit, OnDestroy} from '@angular/core';

import { Subscription } from 'rxjs';
import {Sort, MatSortModule} from '@angular/material/sort';

import { Event } from '../../models/event';

import { BajajEventsService } from '../../services/bajaj-events.service';


@Component({
  selector: 'bajaj-events-list',
  templateUrl: './events-list.component.html',
  styleUrl: './events-list.component.css',
})
export class EventsListComponent implements OnInit, OnDestroy{
  constructor(private _eventsService : BajajEventsService){}
  title: string = 'Welcome to Bajaj Finserv Events List!';
  subtitle: string = 'Published by Bajaj Finserv HR team';
  events: Event[];
  // sortedData: Event[]
  mapUrl: string;
  // selectedEventId: number;
  searchEventcharacters: string = '';
  totalItemsPerPage: number = 2;
  CurrentPage: number = 1;
  _eventServiceSubscription : Subscription;
  sortedColumn: keyof Event | undefined;
  isAscending: boolean = true;
  ngOnInit(): void {
    this._eventServiceSubscription= this._eventsService.getAllEvents().subscribe({
      next:data=>this.events = data,
      error:err=>console.log(err)
    });
    // this.sortedData = this.events.slice();
  }
  
  // onEventSelection(eventId: number): void {
  //   this.selectedEventId = eventId;
  // }
  getMap(event: Event): void {
    if (event.venue == 'IBIS Hotel Vimaan Nagar')
      this.mapUrl =
        'https://www.google.co.in/maps/place/ibis+Pune+Viman+Nagar/@18.5558895,73.9088665,2848m/data=!3m2!1e3!5s0x3bc2c1407dc761af:0xa576a7946a2cb7e0!4m9!3m8!1s0x3bc2c13fdae5b205:0x2f1a55f89a8b981b!5m2!4m1!1i2!8m2!3d18.5598737!4d73.9130458!16s%2Fg%2F1vgw_5xx?entry=ttu';
    if (event.venue == 'Bajaj Finserv Vimaan Nagar')
      this.mapUrl =
        'https://www.google.co.in/maps/place/Bajaj+Finserv+Health+Limited/@18.5635971,73.920772,786m/data=!3m2!1e3!4b1!4m6!3m5!1s0x3bc2c3e034dd1105:0xe37b15f80ea47e31!8m2!3d18.563592!4d73.9233469!16s%2Fg%2F11l7y7cvsq?entry=ttu';
  }
  filterEventsByName(): Event[] {
    return this.events.filter((event) =>
      event.eventName
        .toLocaleLowerCase()
        .includes(this.searchEventcharacters.toLocaleLowerCase())
    );
  }
  changePageNumber(): void {
    this.CurrentPage = 1;
  }
  ngOnDestroy(): void {
    if(this._eventServiceSubscription) this._eventServiceSubscription.unsubscribe();
  }
  sortTable(column: keyof Event) {
    if (this.sortedColumn === column) {
      this.isAscending = !this.isAscending;
    } else {
      this.sortedColumn = column;
      this.isAscending = true;
    }
    this.events.sort((a, b) => {
      const aValue = this.sortedColumn ? a[this.sortedColumn] : undefined;
      const bValue = this.sortedColumn ? b[this.sortedColumn] : undefined;

      if (aValue !== undefined && bValue !== undefined) {
        if (typeof aValue === 'string' && typeof bValue === 'string') {
          return this.isAscending ? aValue.localeCompare(bValue) : bValue.localeCompare(aValue);
        }
        const aValueNumeric = Number(aValue);
        const bValueNumeric = Number(bValue);

        if (!isNaN(aValueNumeric) && !isNaN(bValueNumeric)) {
          return this.isAscending ? aValueNumeric - bValueNumeric : bValueNumeric - aValueNumeric;
        }
      }

      return 0;
    });
  }
}

//   sortData(sort: Sort) {
//     const data = this.events.slice();
//     if (!sort.active || sort.direction === '') {
//       this.sortedData = data;
//       console.log(this.sortedData);
      
//       return;
//     }

//     this.sortedData = data.sort((a, b) => {
//       const isAsc = sort.direction === 'asc';
//       switch (sort.active) {
//         case 'eventCode':
//           return compare(a.eventCode, b.eventCode, isAsc);
//         case 'eventName':
//           return compare(a.eventName, b.eventName, isAsc);
//         case 'fees':
//           return compare(a.fees, b.fees, isAsc);
//         default:
//           return 0;
//       }
//     });
//   }

// function compare(a: number | string, b: number | string, isAsc: boolean) {
//   return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
// }