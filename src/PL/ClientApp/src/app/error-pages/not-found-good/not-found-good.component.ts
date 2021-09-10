import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-not-found-good',
  templateUrl: './not-found-good.component.html',
  styleUrls: ['./not-found-good.component.css']
})
export class NotFoundGoodComponent implements OnInit {
  public notFoundText: string = `Неможливо знайти такий продукт!!!`;

  constructor() { }

  ngOnInit() {
  }

}
