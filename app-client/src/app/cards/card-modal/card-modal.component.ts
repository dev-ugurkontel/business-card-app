import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Card } from 'src/app/models/card';
import { CardService } from 'src/app/services/card.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-card-modal',
  templateUrl: './card-modal.component.html',
  styleUrls: ['./card-modal.component.scss']
})
export class CardModalComponent implements OnInit {

  cardForm!: FormGroup;
  showSpinner: boolean = false;

  constructor(
    private fb: FormBuilder,
    private CardService: CardService,
    private dialogRef: MatDialogRef<CardModalComponent>,
    private SnackbarService: SnackbarService,
    @Inject(MAT_DIALOG_DATA) public data: Card
  ) { }

  ngOnInit(): void {
    console.log(this.data);
    this.cardForm = this.fb.group({
      name: [this.data?.name || '', [Validators.required, Validators.maxLength(50)]],
      title: [this.data?.title || '', Validators.maxLength(255)],
      phone: [this.data?.phone || '', [Validators.required, Validators.maxLength(20)]],
      email: [this.data?.email || '', [Validators.required, Validators.email, Validators.maxLength(50)]],
      address: [this.data?.address || '', Validators.maxLength(255)],
    });
  }

  addCard(): void {
    this.showSpinner = true;
    this.CardService.addCard(this.cardForm.value)
      .subscribe((res: any) => {
        this.getSuccess('Kartvizit başarıyla eklendi.');
      }, (err: any) => {
        this.getError('Eklenme işlemi sırasında bir hata oluştu.');
      });
  }

  updateCard(): void {
    this.showSpinner = true;
    this.CardService.updateCard(this.cardForm.value, this.data.id)
      .subscribe((res: any) => {
        this.getSuccess('Kartvizit başarıyla güncellendi.');
      }, (err: any) => {
        this.getError('Güncellenme işlemi sırasında bir hata oluştu.');
      });
  }

  deleteCard(): void {
    this.showSpinner = true;
    this.CardService.deleteCard(this.data.id)
      .subscribe((res: any) => {
        this.getSuccess('Kartvizit başarıyla silindi.');
      }, (err: any) => {
        this.getError('Silinme işlemi sırasında bir hata oluştu.');
      });
  }

  getSuccess(message: string): void {
    this.SnackbarService.createSnackbar('success', message);
    this.CardService.getCards();
    this.showSpinner = false;
    this.dialogRef.close();
  }

  getError(message: string): void {
    this.SnackbarService.createSnackbar('error', message);
    this.showSpinner = false;
  }

}
