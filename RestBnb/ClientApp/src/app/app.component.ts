import { Component } from '@angular/core';
import { Property } from './interfaces/property.model';
import { HttpService } from './services/http.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public properties: Property[];

  constructor(private httpService: HttpService) { }

  public getProperties: () => void = () => {
    const route = 'https://localhost:5001/api/v1/properties';

    this.httpService.getData(route)
      .subscribe((result) => {
        this.properties = result as Property[];
      },
        (error) => {
          console.log(error);
        });
  }
}
