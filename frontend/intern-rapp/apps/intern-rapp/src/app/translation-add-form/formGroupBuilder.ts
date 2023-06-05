import {
  FormGroup,
  FormControl,
  Validators,
  FormArray,
  MaxValidator,
} from '@angular/forms';
import { InternshipTranslation } from '../entities/internshipTranslation';
import { InternshipTranslationUpdateDto } from '../entities/internshipTranslationUpdateDto';
import { InternshipTranslationUpdatePostDto } from '../entities/internshipTranslationUpdatePostDto';
export function convertFormsArrayToObjectForNewInternship(
  formArray: FormArray
): InternshipTranslation[] {
  const tranformedVersions: InternshipTranslation[] = [];

  formArray.controls.forEach((x) => {
    tranformedVersions.push({
      comment: (x as FormGroup).controls['comment'].value,
      description: (x as FormGroup).controls['description'].value,
      knowledgeToDevelop: (x as FormGroup).controls['knowledgeToDevelop'].value,
      languageId: (x as FormGroup).controls['languageId'].value,
      neededKnowledge: (x as FormGroup).controls['neededKnowledge'].value,
      titleContent: (x as FormGroup).controls['titleContent'].value,
    });
  });
  return tranformedVersions;
}
export function convertFormsArrayToObjectForUpdatedInternship(
  formArray: FormArray
): InternshipTranslationUpdatePostDto[] {
  const tranformedVersions: InternshipTranslationUpdatePostDto[] = [];

  formArray.controls.forEach((x) => {
    tranformedVersions.push({
      comment: (x as FormGroup).controls['comment'].value,
      description: (x as FormGroup).controls['description'].value,
      knowledgeToDevelop: (x as FormGroup).controls['knowledgeToDevelop'].value,
      languageId: (x as FormGroup).controls['languageId'].value,
      translationId: (x as FormGroup).controls['translationId'].value,
      neededKnowledge: (x as FormGroup).controls['neededKnowledge'].value,
      titleContent: (x as FormGroup).controls['titleContent'].value,
    });
  });
  return tranformedVersions;
}
export function buildFormGroupForTranslations(
  data: InternshipTranslationUpdateDto | undefined,
  languageId: number | undefined = undefined,
  languageCode: string | undefined = undefined
) {
  return new FormGroup({
    titleContent: new FormControl(data?.titleContent ?? '', [
      Validators.required,
    ]),
    description: new FormControl(data?.description ?? '', [
      Validators.required,
      Validators.maxLength(1000),
    ]),
    knowledgeToDevelop: new FormControl(data?.knowledgeToDevelop ?? '', [
      Validators.required,
      Validators.maxLength(1000),
    ]),
    comment: new FormControl(data?.comment ?? '', [Validators.required]),
    neededKnowledge: new FormControl(data?.neededKnowledge ?? '', [
      Validators.required,
      Validators.maxLength(1000),
    ]),
    languageId: new FormControl(data?.language.id ?? languageId, [
      Validators.required,
      Validators.maxLength(1000),
    ]),
    languageCode: new FormControl(data?.language.code ?? languageCode, [
      Validators.required,
      Validators.maxLength(1000),
    ]),
    translationId: new FormControl(data?.translationId ?? 0, [
      Validators.required,
      Validators.maxLength(1000),
    ]),
  });
}
