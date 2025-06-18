<?php


// Created by Atikin and Raine_


$notes = [
    '1' => ['name' => 'C4', 'freq' => 261.63],
    '2' => ['name' => 'D4', 'freq' => 293.66],
    '3' => ['name' => 'E4', 'freq' => 329.63],
    '4' => ['name' => 'F4', 'freq' => 349.23],
    '5' => ['name' => 'G4', 'freq' => 392.00],
    '6' => ['name' => 'A4', 'freq' => 440.00],
    '7' => ['name' => 'B4', 'freq' => 493.88],
];

$sampleRate = 44100; 
$duration = 1.0;
$amplitude = 32760; 

function generateSineWave($freq, $sampleRate, $duration, $amplitude)
{
    $samples = [];
    $numSamples = $sampleRate * $duration;

    for ($i = 0; $i < $numSamples; $i++) {
        $time = $i / $sampleRate;
        $value = $amplitude * sin(2 * M_PI * $freq * $time);
        $samples[] = (int) $value; 
    }

    return $samples;
}

function createWavFile($samples, $sampleRate, $filename)
{
    $numSamples = count($samples);
    $numChannels = 1; 
    $bitsPerSample = 16;
    $byteRate = $sampleRate * $numChannels * $bitsPerSample / 8;
    $blockAlign = $numChannels * $bitsPerSample / 8;

    $header = pack(
        'A4VA4A4VvvVVvvA4V',
        'RIFF', 
        36 + $numSamples * 2, 
        'WAVE', 
        'fmt ', 
        16, 
        1, 
        $numChannels, 
        $sampleRate, 
        $byteRate, 
        $blockAlign, 
        $bitsPerSample, 
        'data', 
        $numSamples * 2 
    );

    $data = '';
    foreach ($samples as $sample) {
        $data .= pack('s', $sample);
    }

    file_put_contents($filename, $header . $data);
}

function playWav($filename)
{
    if (strtoupper(substr(PHP_OS, 0, 3)) === 'WIN') {
        exec('start /B "" "' . $filename . '"'); 
    } else {
        exec('aplay ' . $filename . ' 2>/dev/null'); 
    }
}

echo "Simple PHP Piano\n";
echo "Press 1-7 to play C4-B4, or 'q' to quit.\n";

while (true) {
   $input = trim(fgets(STDIN));
  

    if ($input === 'q') {
        echo "Exiting...\n";
        break;
    }

    if (isset($notes[$input])) {
        $note = $notes[$input];
        echo "Playing {$note['name']} ({$note['freq']} Hz)...\n";

        $samples = generateSineWave($note['freq'], $sampleRate, $duration, $amplitude);
        $filename = 'note.wav';
        createWavFile($samples, $sampleRate, $filename);

        playWav($filename);
    } else {
        echo "Invalid input. Use 1-7 or 'q' to quit.\n";
    }
}

?>