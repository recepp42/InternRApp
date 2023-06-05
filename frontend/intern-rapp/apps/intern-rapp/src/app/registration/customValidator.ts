import { AbstractControl, FormGroup, ValidatorFn } from "@angular/forms";

export function isValidSubmitPassword(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const value = control.value;
    if (control.parent) {
      const parentFormGroup = control.parent as FormGroup;
      if (value === parentFormGroup.get('password')?.value) {
        return null;
      }
    }
    return {
      customValidatorlessThanOrEqualToMaxStudents: {
        passWordInvalid:
          'invalidSubmitPassword',
      },
    };
  };
}
