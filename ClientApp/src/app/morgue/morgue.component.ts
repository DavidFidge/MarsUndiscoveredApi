import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {MorgueInterface} from "../morgueInterface";

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
    this.httpClient.get<MorgueInterface[]>(this.baseUrl + 'api/morgue').subscribe(morgueItems => {

      for (const morgueItem of morgueItems) {
        let morgue = new Morgue(morgueItem);
        this.morgues.push(morgue);
      }

    }, error => console.error(error));
  }
}

export class Morgue {
  constructor(morgueInterface: MorgueInterface) {
    this.id = morgueInterface.id;
    this.username = morgueInterface.username;

    this.seed = "";

    if (morgueInterface.seed) {
      let seed = morgueInterface.seed.substring(0, 20);

      if (seed.length != morgueInterface.seed.length)
        seed = seed + "...";

      this.seed = seed;
    }

    let date = new Date(morgueInterface.endDate);
    this.endDate = date.toDateString();
    this.fate = morgueInterface.isVictorious ? "Won" : "Lost";
    this.gameEndStatus = morgueInterface.gameEndStatus;
  }

  id: string;
  endDate: string;
  username: string;
  seed: string;
  fate: string;
  gameEndStatus: string;
}

