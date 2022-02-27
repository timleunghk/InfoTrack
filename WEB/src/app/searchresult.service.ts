
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchViewModel } from './search-view-model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SearchresultService {

  API_URL = environment.baseUrl;

  constructor(private httpClient: HttpClient) { }

  getResult(data: any): Observable<SearchViewModel> {
      var targeturl= data.targetUrl.replace('https://','');
      var targeturl=targeturl.replace('http://','');

      console.log(targeturl);
      return this.httpClient.post<SearchViewModel>(this.API_URL+ "/Search/?keywords=" + data.keywords + "&targetUrl=" + targeturl, data);
  }

}
