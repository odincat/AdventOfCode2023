use std::{fs::read_to_string, io::{BufReader, self}, collections::HashMap, default, ops::Range};

type Mapping = HashMap<Range<u64>, Range<u64>>;

fn main() -> io::Result<()> {
    let input = read_to_string("./input.txt")?;

    let mut seeds: Vec<u64> = vec![];

    let mut seed_soil_mappings: Mapping = HashMap::new();
    let mut soil_fertalizer_mappings: Mapping = HashMap::new();
    let mut fertalizer_water_mappings: Mapping = HashMap::new();
    let mut water_light_mappings: Mapping = HashMap::new();
    let mut light_temperature_mappings: Mapping = HashMap::new();
    let mut temperature_humidity_mappings: Mapping = HashMap::new();
    let mut humidity_location_mappings: Mapping = HashMap::new();

    seed_soil_mappings.insert(98..100, 50..52);
    seed_soil_mappings.insert(50..98, 52..100);

    let goal = 99;

    let x = get_destination_from_source(&seed_soil_mappings, goal);

    let mut currentMappingRef: &mut Mapping = &mut seed_soil_mappings;

    for line in input.lines() {
        if line.is_empty() {
            continue;
        }

        if line.starts_with("seeds: ") {
            let nums: Vec<&str> = line.split(": ").collect();

            seeds = nums.last().unwrap().split(" ").map(|x| x.parse::<u64>().unwrap()).collect();
        } else if line.ends_with(":") {
            let split: Vec<_> = line.split(" ").collect();

            let name = split.first().unwrap().to_owned();

            match name {
                "seed-to-soil" => {
                    currentMappingRef = &mut seed_soil_mappings;
                }

                "soil-to-fertilizer" => {
                    currentMappingRef = &mut soil_fertalizer_mappings;
                }

                "fertilizer-to-water" => {
                    currentMappingRef = &mut fertalizer_water_mappings;
                }

                "water-to-light" => {
                    currentMappingRef = &mut water_light_mappings;
                }

                "light-to-temperature" => {
                    currentMappingRef = &mut light_temperature_mappings;
                }

                "temperature-to-humidity" => {
                    currentMappingRef = &mut temperature_humidity_mappings;
                }

                "humidity-to-location" => {
                    currentMappingRef = &mut humidity_location_mappings;
                }

                _ => {
                    todo!();
                }
            }
        } else {
            let parts: Vec<&str> = line.split(" ").collect();

            let destination_start = parts.first().unwrap();
            let source_start = parts.get(1).unwrap();
            let range = parts.last().unwrap();

            let destination_start: u64 = destination_start.parse().unwrap();
            let source_start: u64 = source_start.parse().unwrap();
            let range: u64 = range.parse().unwrap();

            currentMappingRef.insert(source_start..source_start + range, destination_start..destination_start + range);
        }
    }


    let mut nums: Vec<u64> = vec![];

    for seed in seeds {
        let soil = get_destination_from_source(&seed_soil_mappings, seed);
        let fertalizer = get_destination_from_source(&soil_fertalizer_mappings, soil);
        let water = get_destination_from_source(&fertalizer_water_mappings, fertalizer);
        let light = get_destination_from_source(&water_light_mappings, water);
        let temperature = get_destination_from_source(&light_temperature_mappings, light);
        let humidity = get_destination_from_source(&temperature_humidity_mappings, temperature);
        let location = get_destination_from_source(&humidity_location_mappings, humidity);

        nums.push(location);
    }
    
    nums.sort();

    dbg!(nums.first().unwrap());

    Ok(())
}

fn get_destination_from_source(mapping: &Mapping, target: u64) -> u64 {
    let mut value = target;

    for (source, destination) in mapping {
        if source.contains(&target) {
            let min = source.clone().min().unwrap();

            let index = target - min;

            value = destination.clone().nth(index as usize).unwrap();
        }
    }

    value
}
