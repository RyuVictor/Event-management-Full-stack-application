import { Component } from '@angular/core';
import { RegistrationForm } from '../../models/registration-form';

import { BajajEventsService } from '../../services/bajaj-events.service';

@Component({
  selector: 'bajaj-register-event',
  templateUrl: './register-event.component.html',
  styleUrl: './register-event.component.css'
})
export class RegisterEventComponent {
  constructor(private _bajajEventsService : BajajEventsService ){}
  title : string = "Bajaj Events registration form!"
  eventRegistrationModel : RegistrationForm = new RegistrationForm();
  onEventSubmit(): void {
    this._bajajEventsService.regiserEvent(this.eventRegistrationModel.eventForm.value).subscribe({
      next: data => {
        console.log(data);
      }
    });
  }
}
