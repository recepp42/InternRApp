import { PrefaceTranslationForUpdateData } from "./prefaceTranslationForUpdateData";

export interface DepartmentUpdate {
  id: number;
  name: string;
  managerEmails: string[];
  prefaceTranslations: PrefaceTranslationForUpdateData[] | null;
}