import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import { InternshipTranslationUpdateDto } from "../entities/internshipTranslationUpdateDto";
import { PrefaceTranslationCreateUnit } from "../entities/prefaceTranslationCreateUnit";
import { DepartmentItemDetail } from "../entities/departmentItemDetail";
import { PrefaceTranslationUpdateUnit } from "../entities/prefaceTranslationUpdateUnit";
import { PrefaceTranslationForUpdateData } from "../entities/prefaceTranslationForUpdateData";

// export function convertFormsArrayToObjectForNewUnit(formArray: FormArray): InternshipTranslation[]
// {

// }
export function buildFormGroupForTranslations(
  data: PrefaceTranslationUpdateUnit | undefined,
  languageId: number | undefined = undefined,
  languageName: string | undefined = undefined
) {
  return new FormGroup({
    content: new FormControl(data?.content ?? '', [
      Validators.required,
    ]),
    languageId: new FormControl(data?.language.id ?? languageId, [
      Validators.required,
    ]),
    languageName: new FormControl(data?.language.name ?? languageName, [
      Validators.required,
    ]),
    translationId: new FormControl(data?.translationId ?? 0, [
      Validators.required,
    ]),
  });
}
export function convertFormsArrayToObjectForNewUnit(
  formArray: FormArray
): PrefaceTranslationCreateUnit[] {
  const tranformedVersions: PrefaceTranslationCreateUnit[] = [];

  formArray.controls.forEach((x) => {
    tranformedVersions.push({
      content: (x as FormGroup).controls['content'].value,
      languageId:(x as FormGroup).controls['languageId'].value
    });
  });
  return tranformedVersions;
}
export function convertFormsArrayToObjectForUpdatedUnit(formArray: FormArray): PrefaceTranslationForUpdateData[] {
  const tranformedVersions: PrefaceTranslationForUpdateData[] = []
  
  formArray.controls.forEach(x => {
    tranformedVersions.push({
      content: (x as FormGroup).controls['content'].value,
      languageId: (x as FormGroup).controls['languageId'].value,
      translationId: (x as FormGroup).controls['translationId'].value,
    })
  })
    return tranformedVersions
  }
