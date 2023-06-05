import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule } from '@ngx-translate/core';
import {
  AngularEditorConfig,
  AngularEditorModule,
} from '@kolkov/angular-editor';

@Component({
  selector: 'intern-rapp-preface-translation-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    TranslateModule,
    AngularEditorModule,
  ],
  templateUrl: './preface-translation-form.component.html',
  styleUrls: ['./preface-translation-form.component.scss'],
})
export class PrefaceTranslationFormComponent {
  @Input() unitPrefaceTranslationForm: FormGroup | undefined;
  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: 'auto',
    minHeight: '0',
    maxHeight: 'auto',
    width: 'auto',
    minWidth: '0',
    translate: 'yes',
    enableToolbar: true,
    showToolbar: true,
    placeholder: '',
    defaultParagraphSeparator: '',
    // defaultFontName: '',
    // defaultFontSize: '12',
    // fonts: [{ class: 'Verdana', name: 'Verdana' }],
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
