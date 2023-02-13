import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MorgueReportInterface } from "../morgueReportInterface";

@Component({
  selector: 'app-morgue-item',
  templateUrl: './morgue-item.component.html',
  styleUrls: ['./morgue-item.component.css']
})
export class MorgueItemComponent implements OnInit {

  public morgueReport: MorgueReport | undefined;
  private httpClient: HttpClient;
  private baseUrl: string;
  private route;
  constructor(routeArg: ActivatedRoute, httpClientArg: HttpClient, @Inject('BASE_URL') baseUrlArg: string) {
    this.route = routeArg;
    this.httpClient = httpClientArg
    this.baseUrl = baseUrlArg;
  }
  ngOnInit() {
    // First get the product id from the current route.
    const routeParams = this.route.snapshot.paramMap;
    const idFromRoute = routeParams.get('id');

    this.httpClient.get<MorgueReportInterface>(this.baseUrl + 'api/morgue/' + idFromRoute).subscribe(morgueReportItem => {
      this.morgueReport = new MorgueReport(morgueReportItem);

    }, error => console.error(error));
  }
}

export class MorgueReport {
  constructor(morgueItem: MorgueReportInterface) {
    this.report = morgueItem.textReport;
    this.username = morgueItem.username;

    let date = new Date(morgueItem.endDate);
    this.date = date.toDateString();
  }

  public report: string;
  username: string;
  date: string;
}
