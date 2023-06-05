export interface InternshipItem {
  id: number;
  title: string;
  unitName: string;
  maxStudents: number;
  currentCountOfStudents: number;
  creatorId: number | null;
}