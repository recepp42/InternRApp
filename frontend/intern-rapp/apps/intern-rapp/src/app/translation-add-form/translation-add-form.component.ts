import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { AngularEditorConfig, AngularEditorModule } from '@kolkov/angular-editor';
import { MatTooltipModule } from '@angular/material/tooltip';
@Component({
  selector: 'intern-rapp-translation-add-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    AngularEditorModule,
    TranslateModule,
    MatInputModule,
    MatDialogModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './translation-add-form.component.html',
  styleUrls: ['./translation-add-form.component.scss'],
})
export class TranslationAddFormComponent {

  @Input() internshipTranslationForm: FormGroup | undefined;
  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '250px',
    minHeight: '0',
    maxHeight: '200px',
    width: '500px', 
    minWidth: 'auto',
    
    translate: 'yes',
    enableToolbar: true,
    showToolbar: true,
    placeholder: '',
    defaultParagraphSeparator: '',
    toolbarHiddenButtons: [
      [
        'undo',
        'redo',
        'underline',
        'fonSize',
        'strikeThrough',
        'subscript',
        'superscript',
        'justifyLeft',
        'justifyCenter',
        'justifyRight',
        'justifyFull',
        'indent',
        'outdent',
        'fontName',
      ],
      [
        'fontSize',
        'textColor',
        'backgroundColor',
        'link',
        'unlink',
        'insertImage',
        'insertVideo',
        'insertHorizontalRule',
        'removeFormat',
        'toggleEditorMode',
      ],
    ],
  };
}
