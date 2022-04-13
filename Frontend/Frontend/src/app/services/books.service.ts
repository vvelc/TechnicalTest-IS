import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  url = 'https://localhost:7067/api/Books'

  constructor(private httpclient: HttpClient) {}

  // Create Books
  createBook(body: any): Observable<any> {
    return this.httpclient.post(this.url, body)
  }

  // Get All Books
  getBooks(): Observable<any> {
    return this.httpclient.get(this.url)
  }

  // Get One Book
  getBook(id: number): Observable<any> {
    return this.httpclient.get(`${this.url}/${id}`)
  }

  // Update One Book
  updateBook(id: number, body: any): Observable<any> {
    return this.httpclient.put(`${this.url}/${id}`, body)
  }

  // Delete One Book
  deleteBook(id: number): Observable<any> {
    return this.httpclient.delete(`${this.url}/${id}`)
  }
}
