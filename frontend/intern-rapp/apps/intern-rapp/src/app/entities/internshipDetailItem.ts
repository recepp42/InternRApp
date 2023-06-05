import { TrainingType } from '../enums/trainingType';
import { DepartmentWithIdOnly } from './departmentWithIdOnly';
import { InternshipTranslationUpdateDto } from './internshipTranslationUpdateDto';
import { LocationItem } from './locationItem';

export interface InternshipDetailItem {
  schoolYear: string;
  internShipId: number;
  unitId: DepartmentWithIdOnly;
  maxCountOfStudents: number;
  currentCountOfStudents: number;
  trainingType: TrainingType;
  locations: LocationItem[];
  versions: InternshipTranslationUpdateDto[];
}
