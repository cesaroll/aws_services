CREATE TABLE "Customers" (
    "Id" UUID PRIMARY KEY,
    "FullName" VARCHAR(100) NOT NULL,
    "Email" VARCHAR(100) NOT NULL,
    "GitHubUsername" VARCHAR(100) NOT NULL,
    "DateOfBirth" TIMESTAMP WITH TIME ZONE NOT NULL
);
