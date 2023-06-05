import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { HttpClientModule } from '@angular/common/http';
import { MatIconModule } from '@angular/material/icon';
import { LanguageItem } from '../entities/languageItem';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'intern-rapp-language-update-popup',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatDividerModule,
    TranslateModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    HttpClientModule,
    MatIconModule,
  ],
  templateUrl: './language-update-popup.component.html',
  styleUrls: ['./language-update-popup.component.scss'],
})
export class LanguageUpdatePopupComponent {
  @Input() data!: LanguageItem;
  updateForm: FormGroup | undefined;
  managerEmailsAfterUpdate: string[] = [];
  constructor(public dialogRef: MatDialogRef<LanguageUpdatePopupComponent>) {}
  ngOnInit(): void {
    this.updateForm = new FormGroup({
      languageName: new FormControl('', [Validators.required]),
      languageCode: new FormControl('', [Validators.required]),
    });
    this.updateForm.controls['languageName'].patchValue(this.data?.name);
    this.updateForm.controls['languageCode'].patchValue(this.data?.code);
  }

  close(submit: boolean) {
    this.data.name = this.updateForm?.controls['languageName'].getRawValue();
    this.data.code = this.updateForm?.controls['languageCode'].getRawValue();
    this.dialogRef.close(submit ? this.data : undefined);
  }
}
