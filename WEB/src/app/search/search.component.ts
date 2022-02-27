import { Component, OnInit } from '@angular/core';
import { SearchresultService } from '../searchresult.service';
import { FormGroup, FormControl, Validators, FormBuilder, NgForm} from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner"; 
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {


  public searchViewDetail:any = [];
  public isSubmitted=false;

  form = new FormGroup({
    keywords: new FormControl('', Validators.required),
    url: new FormControl('', [Validators.required,  Validators.pattern('(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?')]),
  });


  constructor(private searchResultService: SearchresultService,
    private fb: FormBuilder,
    private SpinnerService: NgxSpinnerService,
    private animation:BrowserAnimationsModule) { 
      
  };

  get f(){
    return this.form.controls;
  }
 
  ngOnInit(): void {
  
  }

  searchData() { 
    this.isSubmitted=true;
    this.SpinnerService.show();

    let data = {  
      keywords: this.form.controls['keywords'].value,  
      targetUrl:this.form.controls['url'].value
    };  

    console.log(data);
  
    
    this.searchResultService.getResult(data).subscribe(
     searchView => {
       
        this.searchViewDetail! = searchView.searchResults;
        this.SpinnerService.hide();
      }
    );
  }

  resetForm() {
    this.form.reset();
    
    this.isSubmitted=false;

  }

 


}
