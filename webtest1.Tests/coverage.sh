#!/bin/sh

dotnet test                                             \
    /p:CollectCoverage=true                             \
    /p:CoverletOutput=BuildReports/Coverage/            \
    /p:CoverletOutputFormat=cobertura 

reportgenerator                                             \
    -reports:BuildReports/Coverage/coverage.cobertura.xml   \
    -targetdir:BuildReports/Coverage                        \
    -reporttypes:"HTML;HTMLSummary"