import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarModule, MatSnackBarRef } from '@angular/material/snack-bar';
import { TranslateModule } from '@ngx-translate/core';
@Component({
  selector: 'intern-rapp-snackbar',
  standalone: true,
  imports: [CommonModule, MatSnackBarModule,TranslateModule],
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.scss'],
})
export class SnackbarComponent {
  snackBarRef = Inject(MatSnackBarRef);
}
