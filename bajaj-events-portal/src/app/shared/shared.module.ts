import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DateGlobalizationPipe } from './pipes/date-globalization.pipe';



@NgModule({
  declarations: [
    DateGlobalizationPipe,
  ],
  imports: [
    CommonModule
  ],
  exports:[
    DateGlobalizationPipe
  ]
})
export class SharedModule { }
