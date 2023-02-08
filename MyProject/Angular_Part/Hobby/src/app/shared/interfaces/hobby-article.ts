import { IComment } from './comment';
import { IPhoto } from './photo';
import { ITag } from "./tag";

export interface IHobby{
    id?: number,
    title: string;
    description: string;
    hobbySubcategoryId: number,
    hobbySubCategory: string | undefined,
    username: string | undefined,
    hobbyPhoto: IPhoto[],
    tags: ITag[],
    hobbyComments: IComment[]
}