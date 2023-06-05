import { AbstractControl, FormGroup, ValidatorFn } from "@angular/forms";

export function islessThanOrEqualToMaxStudents(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const value = control.value;
    if (control.parent) {
      const parentFormGroup = control.parent as FormGroup;  
     if (value <= parentFormGroup.get('maxStudents')?.value) {
       return null;
     }  
    }
     return {
       customValidatorlessThanOrEqualToMaxStudents: {
         greaterThanMaxStudentsErrorMessage:
           'greaterThanMaxStudentsErrorMessage',
       },
     }; 
   
   
  };
}
export function isLessThanOrEqualToParam(maxAllowedNumber: number): ValidatorFn{
    return (control: AbstractControl): { [key: string]: any } | null => {
      const value = control.value;
     
      if (  value <= maxAllowedNumber&&value>=0) {
        return null;
      } else {
        return {
          customLessThanOrEqualToParam: {
            greaterThanMaxAllowedError: 'GreaterThanParamLimitError',
          },
        };
      }
    };
}

