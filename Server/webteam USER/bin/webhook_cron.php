#!/usr/bin/env php
<?php

/**
 * @file
 * This script reads and processes the temporary files dropped off by /home/www/webhook/webhook.php script.
 */

define ('TMPDIR', '/home/www/tmp');

$branches = array(
  'master' => '/home/www/website',
);

$files = array();
if (is_dir(TMPDIR) && $handle = opendir(TMPDIR)) {
  while (FALSE !== ($filename = readdir($handle))) {
    if (preg_match('#^webhook_.*\.tmp$#', $filename)) {
      $files[] = TMPDIR . '/' . $filename;
    }
  }
}

foreach ($files as $filename) {
  if ($file = file_get_contents($filename)) {
    if ($payload = json_decode($file)) {
      if ($payload->object_kind == 'push') {
        $commit = $payload->after;
        $branch = preg_replace('|^refs/heads/|', '', $payload->ref);
        if (!empty($branches[$branch])) {
            $dir = $branches[$branch];
            $command = "(cd $dir && git pull)";
            system($command);
        }
        else {
          echo "Pushed commit $commit uses unknown branch $branch. Ignoring." . PHP_EOL;
        }
         unlink($filename);
      }
    }
    else {
      echo "Could not read payload for $filename!" . PHP_EOL;
    }
	unlink($filename);
  }
  else {
    echo "Could not read $filename!" . PHP_EOL;
  }
	unlink($filename);
}
?>
