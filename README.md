# Bandcamp downloader

[![Build Status][build-shield]][build-link]
[![License][license-shield]][license-link]

[build-shield]: https://github.com/metalnem/bandcamp-downloader/actions/workflows/dotnet.yml/badge.svg
[build-link]: https://github.com/metalnem/bandcamp-downloader/actions/workflows/dotnet.yml
[license-shield]: https://img.shields.io/badge/license-MIT-blue.svg?style=flat
[license-link]: https://github.com/metalnem/bandcamp-downloader/blob/master/LICENSE

Command line tool to download albums from your Bandcamp collection in MP3
V0 format. Its primary purpose is to allow you to download the albums you
purchased, but that are no longer available on Bandcamp website. Requires
[.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) to run.

## Usage

```
$ dotnet run
Description:
  List all albums in your Bandcamp collection.

Usage:
  bandcamp-downloader [command] [options]

Options:
  --username <username> (REQUIRED)  Your Bandcamp account username.
  --password <password> (REQUIRED)  Your Bandcamp account password.

Commands:
  download  Download the album with the specified ID.
```

## Examples

```shell
# List all albums in your Bandcamp collection
$ dotnet run --username $USERNAME --password $PASSWORD
3049477697 The Messthetics — The Messthetics and James Brandon Lewis
 561248726 Necrophobic — In The Twilight Grey (24-bit HD audio)
 639550734 MIDNIGHT — Hellish Expectations
1362411941 MIDNIGHT — Complete and Total Hell

# Download the album with the specified ID
$ dotnet run --username $USERNAME --password $PASSWORD --album 3049477697
```
