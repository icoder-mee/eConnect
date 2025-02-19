import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";

@Component({
  selector: 'app-root',
  standalone: true,
  // imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [NavComponent]
})
export class AppComponent implements OnInit {
  http = inject(HttpClient);
  title = 'E-Connect';
  users: any;

  ngOnInit() {
    this.http.get('https://localhost:7283/api/users').subscribe({
      next: (response) => {this.users = response},
      error: (error) => {console.log(error);},
      complete: () => {console.log("Request has completed");}
    })
  }

}
