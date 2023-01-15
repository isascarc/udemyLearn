import { Photo } from "./photo";

export interface Member {
    id: number;
    userName: string;
    photoUrl: string;
    age: number;
    knownAs: string;
    create: Date;
    lastActive: Date;
    gender: string;
    introdduction?: string;
    lookingFor: string;
    interest?: string;
    city: string;
    country: string;
    photos: Photo[];
}

