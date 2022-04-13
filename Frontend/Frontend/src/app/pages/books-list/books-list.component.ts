import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { BooksService } from 'src/app/services/books.service';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DeleteDialogComponent } from 'src/app/components/delete-dialog/delete-dialog.component';
import { Book } from 'src/app/interfaces/book';
import { EditDialogComponent } from 'src/app/components/edit-dialog/edit-dialog.component';
import { AddDialogComponent } from 'src/app/components/add-dialog/add-dialog.component';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.scss']
})
export class BooksListComponent implements OnInit {

  public books: any[];

  public isResponse = false;

  dummybody = {
    "id": 1,
    "title": "string",
    "description": "string",
    "pageCount": 0,
    "excerpt": "string",
    "publishDate": "2022-04-13T16:33:52.251Z"
  }

  columnas: string[] = ['id', 'title', 'edit', 'delete'];

  datos: Book[] = [];
  dataSource:any;

  constructor(
    private booksService: BooksService, 
    private router: Router,
    public dialog: MatDialog
  ) {
    this.books = [];
  }

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  
  ngOnInit(): void {
    this.getBooks()
  }

  async createBook(book: Book) {
    await this.booksService.createBook(book).subscribe(data => {
      console.log(data)
    })
  }

  async getBooks() {
    this.isResponse = false;
    await this.booksService.getBooks().subscribe(data => {
      this.books = data;
      console.log(this.books)
      this.isResponse = true;
      this.dataSource = new MatTableDataSource<Book>(this.books);
      this.dataSource.paginator = this.paginator;
    })
  }

  async getBook() {
    await this.booksService.getBook(1).subscribe(data => {
      this.books = data;
      console.log(this.books)
    })
  }

  async updateBook(id: number, body: Book) {
    await this.booksService.updateBook(1, body)
            //.subscribe(data => console.log(data))
  }

  async deleteBook(id: number) {
    await this.booksService.deleteBook(id)
            //.subscribe(data => console.log(data))
  }

  navigateToBook(id: number) {
    this.router.navigate([`/${id}`])
  }

  editClicked(e:Event, book: Book) {
    e.stopPropagation();
    this.openEditDialog(book);
  }

  deleteClicked(e:Event, book: Book) {
    e.stopPropagation();
    this.openDeleteDialog(book);
  }

  addClicked(e:Event) {
    this.openAddDialog();
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddDialogComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe(result => {
      //console.log('The edit dialog was closed');

      if(result) {
        this.createBook(result).then(() => console.log("Added!"))
      }
    });
  }

  openEditDialog(book: Book): void {
    const dialogRef = this.dialog.open(EditDialogComponent, {
      width: '400px',
      data: book
    });

    dialogRef.afterClosed().subscribe(result => {
      //console.log('The edit dialog was closed');
      if(result) {
        this.updateBook(book.id, book).then(() => console.log("Updated!"))
      }
    });
  }

  openDeleteDialog(book: Book): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '400px',
      data: book
    });

    dialogRef.afterClosed().subscribe(result => {
      //console.log('The delete dialog was closed');
      if(result) {
        this.deleteBook(book.id).then(() => console.log("Deleted!"))
      }
    });
  }
}
