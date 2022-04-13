import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Book } from 'src/app/interfaces/book';

@Component({
  selector: 'app-add-dialog',
  templateUrl: './add-dialog.component.html',
  styleUrls: ['./add-dialog.component.scss']
})
export class AddDialogComponent {

  public book: Book = {
    id: 0,
    title: "",
    description: "",
    pageCount: 0,
    excerpt: "",
    publishDate: new Date("2022-04-12T15:25:02.3279522-04:00")
  }

  constructor(
    public dialogRef: MatDialogRef<AddDialogComponent>,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    this.dialogRef.close(this.book)
  }

}
