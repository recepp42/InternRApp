import { PrefaceTranslationCreateUnit } from "./prefaceTranslationCreateUnit";

export interface DepartmentCreateDto {
  id: number;
  name: string;
  managerEmails: string[];
  prefaces: PrefaceTranslationCreateUnit[] | null;
}