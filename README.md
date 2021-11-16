# Database

This repository presents an example of a database which can be part of the network of [buildingenvelopedata.org](https://www.buildingenvelopedata.org/). Before deploying this repository, [machine](https://github.com/ise621/machine) can be used to set up the machine. The database uses [this API specification](https://github.com/ise621/building-envelope-data).

## Getting started

### On your Linux machine
1. Open your favorite shell, for example, good old
   [Bourne Again SHell, aka, `bash`](https://www.gnu.org/software/bash/),
   the somewhat newer
   [Z shell, aka, `zsh`](https://www.zsh.org/),
   or shiny new
   [`fish`](https://fishshell.com/).
1. Install [Git](https://git-scm.com/) by running
   `sudo apt install git-all` on [Debian](https://www.debian.org/)-based
   distributions like [Ubuntu](https://ubuntu.com/), or
   `sudo dnf install git` on [Fedora](https://getfedora.org/) and closely-related
   [RPM-Package-Manager](https://rpm.org/)-based distributions like
   [CentOS](https://www.centos.org/). For further information see
   [Installing Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).
1. Clone the source code by running
   `git clone git@github.com:ise621/database.git` and navigate
   into the new directory `database` by running `cd database`.
1. Prepare your environment by running `cp .env.sample .env`,
   `cp frontend/.env.local.sample frontend/.env.local`, and adding the line
   `127.0.0.1 local.solarbuildingenvelopes.com` to your `/etc/hosts` file.
1. Install [Docker Desktop](https://www.docker.com/products/docker-desktop), and
   [GNU Make](https://www.gnu.org/software/make/).
1. List all GNU Make targets by running `make help`.
1. Generate and trust a self-signed certificate authority and SSL certificates
   by running `make ssl`.
1. Start all services and follow their logs by running `make up logs`.
1. To see the web frontend navigate to
   `https://local.solarbuildingenvelopes.com:5051` in your web browser, to see
   the GraphQL API navigate to
   `https://local.solarbuildingenvelopes.com:5051/graphql/`, and to see sent
   emails navigate to
   `https://local.solarbuildingenvelopes.com:5051/email/`.

In another shell
1. Drop into `ash` with the working directory `/app`, which is mounted to the
   host's `./backend` directory, inside a fresh Docker container based on
   `./backend/Dockerfile` by running `make shellb`.  If necessary, the Docker
   image is (re)built automatically, which takes a while the first time.
2. List all backend GNU Make targets by running `make help`.
3. For example, update packages and tools by running `make update`.
4. Drop out of the container by running `exit` or pressing `Ctrl-D`.

The same works for frontend containers by running `make shellf`.

## Deployment

### Setting up a Debian production machine
1. Use the sibling project [machine](https://github.com/ise621/machine) and its
   instructions for the first stage of the set-up.
1. Enter a shell on the production machine using `ssh`.
1. Change into the directory `/app` by running `cd /app`.
1. Clone the repository twice by running
   ```
   for environment in staging production ; do
     git clone git@github.com:ise621/database.git ./${environment}
   done
   ```
1. For each of the two environments staging and production referred to by
   `${environment}` below:
   1. Change into the clone `${environment}` by running `cd /app/${environment}`.
   1. Prepare the environment by running `cp .env.${environment}.sample .env`,
      `cp frontend/.env.local.sample frontend/.env.local`, and by replacing
      dummy passwords in the copies by newly generated ones, where random
      passwords may be generated running `openssl rand -base64 32`.
   1. Prepare PostgreSQL by generating new password files by running
      `make --file Makefile.production postgres_passwords`
      and creating the database by running
      `make --file Makefile.production createdb`.

### Creating a release
1. [Draft a new release](https://github.com/ise621/database/actions/workflows/draft-new-release.yml)
   with a new version according to [Semantic Versioning](https://semver.org) by
   running the GitHub action which, in particular, creates a new branch named
   `release/v*.*.*`, where `*.*.*` is the version, and a corresponding pull
   request.
1. Fetch the release branch by running `git fetch` and check it out by running
   `git checkout release/v*.*.*`, where `*.*.*` is the version.
1. Prepare the release by running `make prepare-release` in your shell, review,
   add, commit, and push the changes. In particular, migration and rollback SQL
   files are created in `./backend/src/Migrations/` which need to be reviewed
   --- see
   [Migrations Overview](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
   and following pages for details.
1. [Publish the new release](https://github.com/ise621/database/actions/workflows/publish-new-release.yml)
   by merging the release branch into `main` whereby a new pull request from
   `main` into `develop` is created that you need to merge to finish off.

### Deploying a release
1. Enter a shell on the production machine using `ssh`.
1. Change to the staging envrionment by running `cd /app/staging`.
1. Deploy the new release in the staging environment by running
   `make --file Makefile.production deploy`.
1. If it fails *after* the database backup was made, rollback to the previous
   state by running
   `make --file Makefile.production rollback`,
   figure out what went wrong, apply the necessary fixes to the codebase,
   create a new release, and try to deploy that release instead.
1. If it succeeds, deploy the new reverse proxy that handles sub-domains by
   running `cd ./machine && make deploy && cd ..` and test whether everything
   works as expected and if that is the case, repeat all stages but this one in
   the directory `/app/production` (instead of `/app/staging`). Note that in
   the staging environment sent emails can be viewed in the web browser under
   `https://staging.solarbuildingenvelopes.com/email/` and emails to addresses in
   the variable `RELAY_ALLOWED_EMAILS` in the `.env` file are delivered to the
   respective inboxes (the variable's value is a comma separated list of email
   addresses).

For information on using Docker in production see
[Configure and troubleshoot the Docker daemon](https://docs.docker.com/config/daemon/)
and the pages following it.

## Useful Resources

- [Fullstack Authentication Example with Next.js and NextAuth.js](https://github.com/prisma/prisma-examples/tree/latest/typescript/rest-nextjs-api-routes-auth)
- [NextAuth.js Example App](https://github.com/nextauthjs/next-auth-example)
- [Set up a GraphQL client with Apollo](https://hasura.io/learn/graphql/typescript-react-apollo/apollo-client/)
