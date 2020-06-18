#!/bin/bash

# Exit codes:
#   0: Resource exists
#   1: Resource does not exist

resource_type=$1
resource_name=$2
#command='~/openshift/cli/4.2.0/./oc describe $resource_type $resource_name'
command='oc describe $resource_type $resource_name'

output=$(eval $command 2>&1)

# There's probably a better way to check the reponse, but here goes:
if [[ ${output:0:28} == "Error from server (NotFound)" ]]
then
    echo "The resource '$resource_type\\$resource_name' does not exist"
    exit 1
else
    echo "The resource '$resource_type\\$resource_name' exists"
    exit 0
fi