import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Card } from '../models/card';

@Injectable({
  providedIn: 'root'
})
export class CardService {

  cards!: Card[];

  constructor(
    private http: HttpClient
  ) { }

  getCards(): void {
    this.http.get<Card[]>(`${environment.apiUrl}cards`)
      .subscribe((res: Card[]) => {
        this.cards = res;
      });
  }

  addCard(card: Card): Observable<any> {
    return this.http.post(`${environment.apiUrl}cards`, card);
  }

  updateCard(card: Card, cardId: number): Observable<any> {
    return this.http.put(`${environment.apiUrl}cards/${cardId}`, card);
  }

  deleteCard(cardId: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}cards/${cardId}`);
  }

}
