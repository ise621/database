# Database

This repository presents the reference implementation of a product data server as part of the product data network [buildingenvelopedata.org](https://www.buildingenvelopedata.org/). Before deploying this repository, [machine](https://github.com/building-envelope-data/machine) can be used to set up the machine.

The [API specification of the product data servers](https://github.com/building-envelope-data/api/blob/develop/apis/database.graphql) is available in the repository [api](https://github.com/building-envelope-data/api). There is also a [visualization of the API of a product data server](https://graphql-kit.com/graphql-voyager/?url=https://www.solarbuildingenvelopes.com/graphql/).

This repository is deployed as the [product data server of TestLab Solar Facades of Fraunhofer ISE](https://www.solarbuildingenvelopes.com).

If you have a question for which you don't find the answer in this repository, please raise a [new issue](https://github.com/building-envelope-data/database/issues/new) and add the tag `question`! All ways to contribute are presented by [CONTRIBUTING.md](https://github.com/building-envelope-data/database/blob/develop/CONTRIBUTING.md). The basis for our collaboration is decribed by our [Code of Conduct](https://github.com/building-envelope-data/database/blob/develop/CODE_OF_CONDUCT.md).

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
   `git clone git@github.com:building-envelope-data/database.git` and navigate
   into the new directory `database` by running `cd ./database`.
1. Initialize, fetch, and checkout possibly-nested submodules by running
   `git submodule update --init --recursive`. An alternative would have been
   passing `--recurse-submodules` to `git clone` above.
1. Prepare your environment by running `cp ./.env.sample ./.env`,
   `cp ./frontend/.env.local.sample ./frontend/.env.local`, and adding the line
   `127.0.0.1 local.solarbuildingenvelopes.com` to your `/etc/hosts` file.
1. Install [Docker Desktop](https://www.docker.com/products/docker-desktop), and
   [GNU Make](https://www.gnu.org/software/make/).
1. List all GNU Make targets by running `make help`.
1. Generate and trust a self-signed certificate authority and SSL certificates
   by running `make ssl`. If you are locally working on the metabase and the
   database and if you need them to communicate over HTTPS, then instead of
   running `make ssl`, make the `CERTIFICATE_AUTHORITY_*` variable values in
   the `.env` file match the ones from the metabase, copy the certificate
   authority files from the directories `./ssl`, `./backend/ssl`, and
   `./frontend/ssl` of the metabase project into the respective directories in
   the database project, and run the command `make generate-ssl-certificate`.
1. Generate JSON Web Token (JWT) encryption and signing certificates by running
   `make jwt-certificates`.
1. Start all services and follow their logs by running `make up logs`.
1. To see the web frontend navigate to
   `https://local.solarbuildingenvelopes.com:5051` in your web browser, to see
   the GraphQL API navigate to
   `https://local.solarbuildingenvelopes.com:5051/graphql/`, and to see sent
   emails navigate to
   `https://local.solarbuildingenvelopes.com:5051/email/`. Note that the port is
   `5051` by default. If you set the variable `HTTPS_PORT` within the `./.env`
   to some other value though, you need to use that value instead within the
   URL.

In another shell

1. Drop into `bash` with the working directory `/app`, which is mounted to the
   host's `./backend` directory, inside a fresh Docker container based on
   `./backend/Dockerfile` by running `make shellb`. If necessary, the Docker
   image is (re)built automatically, which takes a while the first time.
1. List all backend GNU Make targets by running `make help`.
1. For example, update packages and tools by running `make update`.
1. Drop out of the container by running `exit` or pressing `Ctrl-D`.

The same works for frontend containers by running `make shellf`.

### Developing with Visual Studio Code

On the very first usage:

1. Install [Visual Studio Code](https://code.visualstudio.com) and open it.
   Navigate to the Extensions pane (`Ctrl+Shift+X`). Add the extension
   [Remote Development](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.vscode-remote-extensionpack).
1. Navigate to the
   [Remote Explorer](https://code.visualstudio.com/docs/devcontainers/containers#_managing-containers)
   pane. Hover over the running `metabase-backend-*` container (if it is not
   running, then run `make up` in a shell inside the project directory) and
   click on the "Attach in Current Window" icon. In the Explorer pane, open the
   directory `/app`, which is mounted to the host's `./backend` directory.
   Navigate to the Extensions pane. Add the extensions
   [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit),
   [IntelliCode for C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscodeintellicode-csharp),
   [GraphQL: Language Feature Support](https://marketplace.visualstudio.com/items?itemName=GraphQL.vscode-graphql),
   and
   [GitLens — Git supercharged](https://marketplace.visualstudio.com/items?itemName=eamodio.gitlens).
1. Navigate to the
   [Remote Explorer](https://code.visualstudio.com/docs/devcontainers/containers#_managing-containers)
   pane. Hover over the running `metabase-frontend-*` container and click on
   the "Attach in New Window" icon. In the Explorer pane, open the directory
   `/app`, which is mounted to the host's `./frontend` directory. Navigate to
   the Extensions pane. Add the extensions
   [GraphQL: Language Feature Support](https://marketplace.visualstudio.com/items?itemName=GraphQL.vscode-graphql),
   and
   [GitLens — Git supercharged](https://marketplace.visualstudio.com/items?itemName=eamodio.gitlens).

Note that the Docker containers are configured in `./docker-compose.yml` in
such a way that Visual Studio Code extensions installed within containers are
retained in Docker volumes and thus remain installed across `make down` and
`make up` cycles.

On subsequent usages: Open Visual Studio Code, navigate to the "Remote
Explorer" pane, and attach to the container(s) you want to work in.

The following Visual Studio Code docs may be of interest for productivity and
debugging

- [Developing inside a Container](https://code.visualstudio.com/docs/devcontainers/containers)
- [Git](https://code.visualstudio.com/docs/sourcecontrol/overview)
- [C#](https://code.visualstudio.com/docs/csharp/navigate-edit)
- [TypeScript](https://code.visualstudio.com/docs/typescript/typescript-tutorial)

#### Debugging

To debug the
[ASP.NET Core web application](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core),
attach Visual Studio Code to the `metabase-backend-*` container,
[press `Ctrl+Shift+P`, select "Debug: Attach to a .NET 5+ or .NET Core process"](https://code.visualstudio.com/docs/csharp/debugging#_attaching-to-a-process),
and choose the process `/app/src/bin/Debug/net9.0/Metabase run` titled
`Metabase` or alternatively navigate to the "Run and Debug" pane
(`Ctrl+Shift+D`), select the launch profile ".NET Core Attach", press the
"Start Debugging" icon (`F5`), and select the same process as above. Then, for
example, open some source files to set breakpoints, navigate through the
website https://local.buildingenvelopedata.org:4041, which will stop at
breakpoints, and inspect the information provided by the debugger at the
breakpoints. For details on debugging C# in Visual Studio Code, see
[Debugging](https://code.visualstudio.com/docs/csharp/debugging).

Note that the debugger detaches after the
[polling file watcher](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-watch#environment-variables)
restarts the process, which happens for example after editing a source file
because `dotnet watch` is configured in `./docker-compose.yml` with
`DOTNET_USE_POLLING_FILE_WATCHER` set to `true`. As of this writing, there is
an
[open feature request to reattach the debugger automatically](https://github.com/dotnet/vscode-csharp/issues/4822).
There also are multiple extensions like
[.NET Watch Attach](https://marketplace.visualstudio.com/items?itemName=Trottero.dotnetwatchattach)
and
[.NET Stalker Debugger](https://marketplace.visualstudio.com/items?itemName=spencerjames.stalker-debugger)
that attempt to solve that. Those extensions don't work in our case though, as
they try to restart `dotnet watch` themselves, instead of waiting for the
polling file watcher of `dotnet watch` to restart
`/app/src/bin/Debug/net9.0/Metabase run` and attach to that process.

## Deployment

For information on using Docker in production see
[Configure and troubleshoot the Docker daemon](https://docs.docker.com/config/daemon/)
and the pages following it.

### Setting up a Debian production machine

1. Use the sibling project [machine](https://github.com/building-envelope-data/machine) and its
   instructions for the first stage of the set-up.
1. Enter a shell on the production machine using `ssh`.
1. Change into the directory `/app` by running `cd /app`.
1. Clone the repository twice by running
   ```
   for environment in staging production ; do
     git clone git@github.com:building-envelope-data/database.git ./${environment}
   done
   ```
1. For each of the two environments staging and production referred to by
   `${environment}` below:
   1. Change into the clone `${environment}` by running `cd /app/${environment}`.
   1. Prepare the environment by running `cp ./.env.${environment}.sample ./.env`,
      `cp ./frontend/.env.local.sample ./frontend/.env.local`, and by replacing
      dummy passwords in the copies by newly generated ones, where random
      passwords may be generated running `openssl rand -base64 32`.
   1. Prepare PostgreSQL by generating new password files by running
      `make --file=Makefile.production postgres_passwords`
      and creating the database by running
      `make --file=Makefile.production createdb`.
   1. Generate JSON Web Token (JWT) encryption and signing certificates by running
      `make --file=Makefile.production jwt-certificates`.

### Creating a release

1. Draft a new release with a new version according to
   [Semantic Versioning](https://semver.org) by running the GitHub action
   [Draft a new release](https://github.com/building-envelope-data/database/actions/workflows/draft-new-release.yml)
   which, creates a new branch named `release/v*.*.*`,
   creates a corresponding pull request, updates the
   [Changelog](https://github.com/building-envelope-data/database/blob/develop/CHANGELOG.md),
   and bumps the version in
   [`package.json`](https://github.com/building-envelope-data/database/blob/develop/frontend/package.json),
   where `*.*.*` is the version. Note that this is **not** the same as "Draft
   a new release" on
   [Releases](https://github.com/building-envelope-data/database/releases).
1. Fetch the release branch by running `git fetch` and check it out by running
   `git checkout release/v*.*.*`, where `*.*.*` is the version.
1. Prepare the release by running `make prepare-release` in your shell, review,
   add, commit, and push the changes. In particular, migration and rollback SQL
   files are created in `./backend/src/Migrations/` which need to be reviewed
   --- see
   [Migrations Overview](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
   and following pages for details.
1. [Publish the new release](https://github.com/building-envelope-data/database/actions/workflows/publish-new-release.yml)
   by merging the release branch into `main` whereby a new pull request from
   `main` into `develop` is created that you need to merge to finish off.

### Deploying a release

1. Enter a shell on the production machine using `ssh`.
1. Back up the production database by running
   `make --directory /app/production --file=Makefile.production BACKUP_DIRECTORY=/app/production/backup backup`.
1. Change to the staging environment by running `cd /app/staging`.
1. Restore the staging database from the production backup by running
   `make --file=Makefile.production BACKUP_DIRECTORY=/app/production/backup restore`.
1. Adapt the environment file `./.env` if necessary by comparing it with the
   `./.env.staging.sample` file of the release to be deployed.
1. Deploy the new release in the staging environment by running
   `make --file=Makefile.production TARGET=${TAG} deploy`, where `${TAG}` is
   the release tag to be deployed, for example, `v1.0.0`.
1. If it fails _after_ the database backup was made, rollback to the previous
   state by running
   `make --file=Makefile.production rollback`,
   figure out what went wrong, apply the necessary fixes to the codebase,
   create a new release, and try to deploy that release instead.
1. If it succeeds, deploy the new reverse proxy that handles sub-domains by
   running `cd /app/machine && make deploy` and test whether everything works
   as expected and if that is the case, continue. Note that in the
   staging environment sent emails can be viewed in the web browser under
   `https://staging.solarbuildingenvelopes.com/email/` and emails to addresses in
   the variable `RELAY_ALLOWED_EMAILS` in the `.env` file are delivered to the
   respective inboxes (the variable's value is a comma separated list of email
   addresses).
1. Change to the production environment by running `cd /app/production`.
1. Adapt the environment file `./.env` if necessary by comparing it with the
   `./.env.staging.sample` file of the release to be deployed.
1. Deploy the new release in the production environment by running
   `make --file=Makefile.production TARGET=${TAG} deploy`, where `${TAG}` is
   the release tag to be deployed, for example, `v1.0.0`.
1. If it fails _after_ the database backup was made, rollback to the previous
   state by running
   `make --file=Makefile.production rollback`,
   figure out what went wrong, apply the necessary fixes to the codebase,
   create a new release, and try to deploy that release instead.

### Troubleshooting

The file `Makefile.production` contains GNU Make targets to manage Docker
containers like `up` and `down`, to follow Docker container logs with `logs`,
to drop into shells inside running Docker containers like `shellb` for the
backend service and `shellf` for the frontend service and `psql` for the
database service, and to list information about Docker like `list` and
`list-services`.

And the file contains GNU Make targets to deploy a new release or rollback it
back as mentioned above. These targets depend on several smaller targets like
`begin-maintenance` and `end-maintenance` to begin or end displaying
maintenance information to end users that try to interact with the website, and
`backup` to backup all data before deploying a new version, `migrate` to
migrate the database, and `run-tests` to run tests.

If for some reason the website displays the maintenance page without
maintenance happening at the moment, then drop into a shell on the production
machine, check all logs for information on what happened, fix issues if
necessary, and end maintenance. It could for example happen that a cron job
set-up by [machine](https://github.com/building-envelope-data/machine) begins
maintenance, fails to do its actual job, and does not end maintenance
afterwards. Whether failing to do its job is a problem for the inner workings
of the website needs to be decided by some developer. If it for example backing
up the database fails because the machine is out of memory at the time of doing
the backup, the website itself should still working.

If the database container restarts indefinitely and its logs say

```
PANIC:  could not locate a valid checkpoint record
```

for example preceded by `LOG:  invalid resource manager ID in primary
checkpoint record` or `LOG:  invalid primary checkpoint record`, then the
database is corrupt. For example, the write-ahead log (WAL) may be corrupt
because the database was not shut down cleanly. One solution is to restore the
database from a backup by running

```
make --file=Makefile.production BACKUP_DIRECTORY=/app/data/backups/20XX-XX-XX_XX_XX_XX/ restore
```

where the `X`s need to be replaced by proper values. Another solution is to
reset the transaction log by entering the database container with

```
docker compose --file=docker-compose.production.yml --project-name metabase_production run database bash
```

and dry-running

```
gosu postgres pg_resetwal --dry-run /var/lib/postgresql/data
```

and, depending on the output, also running

```
gosu postgres pg_resetwal /var/lib/postgresql/data
```

Note that both solutions may cause data to be lost.

## Original Idea

The product identifier service should provide the following endpoints:

- Obtain a new product identifier possibly associating internal meta information with it, like a custom string or a JSON blob
- Update the meta information of one of your product identifiers
- Get meta information of one of your product identifiers
- Get the owner of a product identifier (needed, for example, by the IGSDB to check that the user adding product data with a product identifier owns the identifier)
- List all your product identifiers
- Request the transferal of the ownership of one or all of your product identifiers to another (once the receiving user agrees, the transferal is made)
- Respond to a transferal request

How to obtain a unique product identifier and add product data to some database:

1. Create an account at a central authentication service, that is, a domain specific and lightweight service like [Auth0](https://auth0.com) managed by us (the details of how users prove to be a certain manufacturer are still open)
2. Authenticate yourself at the authentication service receiving a [JSON web token](https://jwt.io) (this could be a simple username/password authentication scheme)
3. Obtain a new product identifier from the respective service passing your JSON web token as means of authentication
4. Add product data to some database like IGSDB passing the product identifier and your JSON web token

JSON web tokens are used for authentication across different requests, services, and domains.

Product identifiers are randomly chosen and verified to be unique 32, 48, or 64 bit numbers, which can be communicated for easy human use as [proquints](https://arxiv.org/html/0901.4016) [there are implementations in various languages](https://github.com/dsw/proquint). We could alternatively use [version 4 universally-unique identifiers](https://tools.ietf.org/html/rfc4122); I consider this to be overkill as it comes with a performance penalty and our identifiers do not need to be universally unique. Either way, [those identifiers do _not_ replace primary keys](https://tomharrisonjr.com/uuid-or-guid-as-primary-keys-be-careful-7b2aa3dcb439).

Randomness of identifiers ensures that

- the product identifier does not encode any information regarding the product, like its manufacturer, which would, for example, be problematic when one manufacturer is bought by another
- a user cannot run out of product identifiers, because there is no fixed range of possible identifiers per user
- it's unlikely that flipping one bit or replacing one letter in the proquint representation by another results in a valid identifier owned by the same user

We may add some error detection and correction capabilities by, for example, generating all but the last 4 bits randomly and using the last 4 bits as [some sort of checksum](https://en.wikipedia.org/wiki/Checksum).

## Useful Resources

- [Set up a GraphQL client with Apollo](https://hasura.io/learn/graphql/typescript-react-apollo/apollo-client/)
