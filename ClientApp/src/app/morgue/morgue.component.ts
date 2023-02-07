import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-morgue',
  templateUrl: './morgue.component.html',
  styleUrls: ['./morgue.component.css']
})

export class MorgueComponent implements OnInit {
  public morgues: Morgue[] = [];

  private httpClient: HttpClient;
  private baseUrl: string;

  constructor(httpClientArg: HttpClient, @Inject('BASE_URL') baseUrlArg: string) {
    this.httpClient = httpClientArg
    this.baseUrl = baseUrlArg;
  }
  ngOnInit(): void {
    this.httpClient.get<Morgue[]>(this.baseUrl + 'api/morgue').subscribe(result => {
      this.morgues = result;
    }, error => console.error(error));
  }
}

export interface Morgue {
  id: string;
  username: string;
  startDate: string;
  endDate: string;
}

