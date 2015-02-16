source params
nuget=$1

date
NuGet SetApiKey $apiKey -source "$source"
NuGet push $nuget -source "$source" -Timeout 6001
date