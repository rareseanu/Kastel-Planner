import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Label } from '../shared/label.model';
import { FormBuilder, FormGroup } from '@angular/forms';
import { RegisterService } from '../shared/register.service';

@Component({
  selector: 'app-label-form',
  templateUrl: './label-form.component.html',
  styleUrls: ['./label-form.component.css']
})
export class LabelFormComponent implements OnInit {

  @Input()
  labelsInDropdown : Label[];
  types$: any;
  labelForm: FormGroup;

  constructor(private registerService: RegisterService, private formBuilder: FormBuilder,) {
    this.displayLabels();
    
      this.labelForm = this.formBuilder.group({
        label: [],
      });
     
   }

  displayLabels():void{  
     this.registerService.getLabelsFromAPI().
      subscribe(data => {
        if(data) {
          this.labelsInDropdown = data;
          console.log(this.labelsInDropdown);
        }
      } );

    
    }

  //methods to get dropdown values
  dropDownLabelName: string = '';
  selectedHandlerLabelName(event : any)
  {
    if(event.target.value != 'default') 
      { this.dropDownLabelName = event.target.value;}
    else 
      {this.dropDownLabelName = '';}

      console.log(this.dropDownLabelName);
  }


  selectedLabel(){
    return this.dropDownLabelName;
  }

  ngOnInit(): void {
     this.displayLabels();
  }
  }
