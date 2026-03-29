/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

export interface BlogDTO {
    id: string;
    title: string;
    content: string;
    coverPhoto: string;
    categories: string[];
    state: number;
    amountOfAccesses: number;
    publishedAt: Date;
    likedOrDislikedByUser: boolean;
    authorId: string;
    authorName: string;
    createdAt: Date;
    updatedAt: Date;
}
