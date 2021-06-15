# Database

## Getting started

### On your Linux machine
1. Open your favorite shell, for example, good old
   [Bourne Again SHell, aka, `bash`](https://www.gnu.org/software/bash/),
   the somewhat newer
   [Z shell, aka, `zsh`](https://www.zsh.org/),
   or shiny new
   [`fish`](https://fishshell.com/).
2. Install [Git](https://git-scm.com/) by running
   `sudo apt install git-all` on [Debian](https://www.debian.org/)-based
   distributions like [Ubuntu](https://ubuntu.com/), or
   `sudo dnf install git` on [Fedora](https://getfedora.org/) and closely-related
   [RPM-Package-Manager](https://rpm.org/)-based distributions like
   [CentOS](https://www.centos.org/). For further information see
   [Installing Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).
3. Clone the source code by running
   `git clone git@github.com:ise621/database.git` and navigate
   into the new directory `database` by running `cd database`.
4. Prepare your environment by running `cp .env.sample .env`,
   `cp frontend/.env.local.sample frontend/.env.local`, and adding the line
   `127.0.0.1 local.testlab-solar-facades.de` to your `/etc/hosts` file.
5. Install [Docker Desktop](https://www.docker.com/products/docker-desktop), and
   [GNU Make](https://www.gnu.org/software/make/).
6. List all GNU Make targets by running `make help`.
7. Generate and trust a self-signed certificate authority and SSL certificates
   by running `make ssl`.
8. Start all services and follow their logs by running `make up logs`.
9. To see the web frontend navigate to
   `https://local.testlab-solar-facades.de:5051` in your web browser and to see
   the GraphQL API navigate to
   `https://local.testlab-solar-facades.de:5051/graphql/`.

In another shell
1. Drop into `ash` with the working directory `/app`, which is mounted to the
   host's `./frontend` directory, inside a fresh Docker container based on
   `./frontend/Dockerfile` by running `make shellf`.  If necessary, the Docker
   image is (re)built automatically, which takes a while the first time.
2. List all frontend GNU Make targets by running `make help`.
3. For example, update packages and tools by running `make update`.
4. Drop out of the container by running `exit` or pressing `Ctrl-D`.

The same works for backend containers by running `make shellb`.

## Useful Resources
- [Fullstack Authentication Example with Next.js and NextAuth.js](https://github.com/prisma/prisma-examples/tree/latest/typescript/rest-nextjs-api-routes-auth)
- [NextAuth.js Example App](https://github.com/nextauthjs/next-auth-example)
- [Set up a GraphQL client with Apollo](https://hasura.io/learn/graphql/typescript-react-apollo/apollo-client/)