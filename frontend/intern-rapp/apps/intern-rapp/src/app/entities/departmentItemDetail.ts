import { PrefaceTranslationUpdateUnit } from './prefaceTranslationUpdateUnit';

export interface DepartmentItemDetail {
  id: number;
  name: string;
  managerEmails: string[];
  prefaceTranslations: PrefaceTranslationUpdateUnit[] | null;
}
