<?php

#config.user.inc.php file

$cfg['Servers'] = [
    1 => [
        'auth_type' => 'config',
        'host' => 'multi-ninja.backend.writemodel.local',
        'user' => 'root',
        'password' => 'secret-password',
    ],
    2 => [
        'auth_type' => 'config',
        'host' => 'multi-ninja.backend.readmodel.local',
        'user' => 'root',
        'password' => 'secret-password',
    ],
];

?>