using System;
using System.Collections.Generic;

namespace EmergencyDispatchSystem
{
    public class Incident
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime CallTimestamp { get; set; }
        public string CallerName { get; set; }
        public string CallerPhone { get; set; }
        public string DispatchNotes { get; set; }

        public string AssignedPriority { get; set; } = "Unassigned";
        public bool IsRejected { get; set; } = false;
        // wondered if it would be good to have calls that can be rejected so i added them anyways (we can remove them easily if you want)
    }

    public static class IncidentLoader
    {
        private static readonly Random _rng = new Random();

        private static readonly List<string> SonderborgLocations = new List<string>
        {
            // Sønderborg City addresses, added them randomly
            "Perlegade 14, 6400 Sønderborg",
            "Jernbanegade 22, 6400 Sønderborg",
            "Kongevej 45, 6400 Sønderborg",
            "Alsgade 68, 6400 Sønderborg",
            "Rønhaveplads 3, 6400 Sønderborg",
            "Bjerggade 11, 6400 Sønderborg",
            "Nørre Havnegade 18, 6400 Sønderborg",
            "Sønder Havnegade 9, 6400 Sønderborg",
            "Østergade 31, 6400 Sønderborg",
            "Vestergade 19, 6400 Sønderborg",
            "Grundtvigs Allé 102, 6400 Sønderborg",
            "Ringgade 54, 6400 Sønderborg",
            "Arnkilgade 27, 6400 Sønderborg",
            "Damgade 40, 6400 Sønderborg",
            "Kastanie Allé 15, 6400 Sønderborg",
            "Alsion 2, 6400 Sønderborg",
            "Ellegårdvej 25, 6400 Sønderborg",
            "Stenager 4, 6400 Sønderborg",
            "Borgmester Andersens Vej 33, 6400 Sønderborg",
            "Sydhavnsgade 12, 6400 Sønderborg",
            "Løkken 8, 6400 Sønderborg",
            "Kær Vestermark 17, 6400 Sønderborg",
            "Sundgade 21, 6400 Sønderborg",
            "Dybbøl Banke 6, 6400 Sønderborg",
            "Chr. X's Bro (Bridge), 6400 Sønderborg",
            "Augustenborg Landevej 88, 6400 Sønderborg",
            "Mommarkvej 114, 6400 Sønderborg",
            "Kegnæsvej 12, 6470 Sydals",

            // Surrounding Region
            "Storegade 19, 6440 Augustenborg",
            "Kettingskov 14, 6440 Augustenborg",
            "Slotsallé 5, 6440 Augustenborg",
            "Storegade 28, 6430 Nordborg",
            "Langesøvej 41, 6430 Nordborg",
            "Havnbjergvej 9, 6430 Nordborg",
            "Nordborgvej 76, 6430 Nordborg",
            "Tandsletvej 30, 6470 Sydals",
            "Høruphavvej 16, 6470 Sydals",
            "Søndre Landevej 52, 6470 Sydals",
            "Borggade 12, 6300 Gråsten",
            "Ulsnæs 24, 6300 Gråsten",
            "Ahlefeldtvej 8, 6300 Gråsten",
            "Felstedvej 19, 6300 Gråsten",
            "Storegade 44, 6310 Broager",
            "Vestergade 15, 6310 Broager",
            "Skeldebro 22, 6310 Broager",
            "Hovedgaden 37, 6400 Vester Sottrup",
            "Nybølvej 11, 6400 Nybøl",
            "Adsbølvej 29, 6300 Gråsten",
            "Sønderborgmotorvejen (E20) Exit 14, 6400 Sønderborg",
            "Alssundbroen (Route 8), 6400 Sønderborg"
        };

        private static readonly List<string> CallerNames = new List<string>
        {
            "Mads Jensen", "Sofie Nielsen", "Mikkel Hansen", "Emma Pedersen",
            "Kasper Andersen", "Freja Christensen", "Lukas Larsen", "Clara Sørensen",
            "Frederik Rasmussen", "Ida Jørgensen", "Oliver Petersen", "Anna Madsen",
            "Magnus Kristensen", "Laura Olsen", "Victor Thomsen", "Mathilde Christiansen",
            "Tobias Poulsen", "Camilla Johansen", "Jonas Møller", "Julie Knudsen",
            "Anonymous Caller", "Automated Alarm System", "Store Security"
        };

        private static readonly List<(string Title, string Description, string DispatchNotes)> IncidentTemplates = new List<(string, string, string)>
        {
            // VIOLENT CRIMES & WEAPONS
            ("Armed Home Invasion", "Multiple masked suspects forced open the front door with a crowbar; one appears to carry a handgun.", "Caller hiding in upstairs bathroom. Line remains open."),
            ("Aggravated Assault", "Two individuals beating a man outside a local pub; victim is unconscious on the pavement.", "Suspects wearing dark hoodies, fleeing toward the harbor."),
            ("Stabbing Reported", "Altercation escalated near the bus stop; one person suffered a stab wound to the abdomen.", "Heavy bleeding reported. Bystanders applying pressure."),
            ("Shots Fired", "Residents report hearing three loud gunshots followed by a dark sedan speeding away.", "No confirmed victims yet. Shell casings possibly on the road."),
            ("Domestic Violence in Progress", "Neighbors hear screaming, crashing glass, and threats of violence from the adjacent apartment.", "History of prior disturbances at this address."),
            ("Brandishing a Firearm", "Man pacing on the sidewalk pointing a pistol at passing cars.", "Suspect wearing a red jacket and jeans."),
            ("Armed Robbery at Gas Station", "Suspect threatened the cashier with a knife and emptied the cash register.", "Fled on foot heading north. Cashier unharmed."),
            ("Kidnapping / Abduction Attempt", "Witness saw two men force a struggling teenager into the back of a white van.", "Partial plate observed: starts with 'CX 42'."),
            ("Bar Brawl", "Large fight involving 10+ patrons spilling out onto the pedestrian street.", "Broken bottles used as weapons."),
            ("Drive-By Shooting", "Vehicle slowed down and fired multiple rounds into a residential living room window.", "Family was inside; miraculously no injuries reported."),

            // HOMICIDE / DECEASED PERSONS
            ("Dead Body Discovered", "Dog walker found an unresponsive male partially hidden in the bushes near the trail.", "Body appears cold; rigor mortis suspected."),
            ("Suspicious Death in Residence", "Landlord entered apartment for inspection and found tenant deceased on the kitchen floor.", "Signs of struggle and overturned furniture."),
            ("Body Washed Ashore", "Kayaker spotted a body floating near the rocky shoreline.", "Coastguard notification may be required."),
            ("Unattended Death", "Elderly resident has not collected mail for two weeks; strong odor coming from inside.", "Door is locked from the inside."),
            ("Fatal Workplace Accident", "Worker crushed beneath a collapsed pallet rack at a local warehouse.", "Co-workers confirm victim has no pulse."),

            // THEFT, BURGLARY & PROPERTY CRIME
            ("Grand Theft Auto", "Brand new electric SUV stolen directly from the driveway while warming up.", "GPS tracker active and moving toward the motorway."),
            ("Commercial Burglary in Progress", "Silent alarm triggered at electronics retailer; CCTV shows two suspects loading TVs.", "Suspects still inside the building."),
            ("Residential Burglary", "Homeowner returned from vacation to find patio door shattered and safe ripped from wall.", "Suspects no longer on scene."),
            ("Catalytic Converter Theft", "Resident heard power tools outside and saw two individuals jacking up a parked van.", "Suspects fled in a silver hatchback."),
            ("Boat Theft from Marina", "Owner arrived at the dock to find their 24-foot motorboat missing from its slip.", "Mooring lines were cleanly cut."),
            ("Shoplifting Escalated to Robbery", "Suspect detained by store security pulled out pepper spray and fled with merchandise.", "Security guard experiencing eye irritation."),
            ("Bicycle Ring / Mass Theft", "Three individuals loading high-end e-bikes from the station rack into a box truck.", "Happening right now."),
            ("Arson / Dumpster Fire", "Commercial dumpster intentionally set ablaze right next to a wooden apartment building.", "Flames spreading to the exterior siding."),
            ("Vandalism / Graffiti in Progress", "Group of youths spray-painting historical monument and smashing streetlights.", "Still at the location."),
            ("ATM Tampering / Skimming", "Bank customer noticed a loose card reader and hidden camera attached to the ATM.", "Suspect seen loitering nearby in a trench coat."),

            // MEDICAL & RESCUE EMERGENCIES
            ("Cardiac Arrest", "62-year-old male collapsed suddenly while jogging; bystander performing CPR.", "AED requested immediately."),
            ("Severe Allergic Reaction", "Child stung by a wasp is experiencing throat swelling and severe difficulty breathing.", "Parents do not have an EpiPen on hand."),
            ("Drug Overdose", "Unconscious male found in public restroom with drug paraphernalia nearby.", "Shallow, agonal breathing."),
            ("Drowning / Person in Water", "Person fell off the pier into freezing water and is struggling to stay afloat.", "Strong current pulling victim away from the dock."),
            ("Stroke Symptoms", "Caller's spouse suddenly has facial drooping, slurred speech, and left-arm weakness.", "Symptoms started 10 minutes ago."),
            ("Child Locked in Hot Car", "Toddler seen locked inside a parked vehicle in direct sunlight; window cracked only slightly.", "Child appears lethargic and flushed."),
            ("Industrial Chemical Spill", "Forklift punctured a drum of corrosive acid inside a manufacturing facility.", "Two workers inhaled fumes and are coughing violently."),
            ("Structural Collapse", "Scaffolding collapsed at a renovation site; two construction workers trapped under debris.", "Dust cloud visible from the street."),
            ("Missing Person (Vulnerable)", "84-year-old with advanced dementia wandered away from care facility without a coat.", "Last seen walking toward the forest."),
            ("Suicide Threat", "Caller states their friend sent a goodbye message and is standing on the bridge railing.", "Urgent intervention required."),

            // TRAFFIC & INFRASTRUCTURE
            ("Head-On Collision", "Two vehicles collided at high speed on a rural two-lane road; both cars heavily deformed.", "Drivers pinned inside vehicles; fuel leaking."),
            ("Hit and Run (Pedestrian)", "Cyclist struck by a speeding delivery van that failed to stop.", "Cyclist has visible leg fracture and head laceration."),
            ("Jackknifed Semi-Truck", "Articulated lorry skidded on wet asphalt, blocking all lanes of traffic.", "Cargo spilling across the roadway."),
            ("Drunk / Reckless Driver", "Driver swerving violently across lanes, nearly hitting oncoming traffic and curbs.", "Vehicle is a dark blue station wagon."),
            ("Downed Live Power Lines", "Tree branch snapped during high winds, knocking high-voltage lines onto a parked car.", "Sparks arcing on the asphalt. Occupant still inside car."),
            ("Gas Main Rupture", "Excavator crew accidentally struck a municipal natural gas line; loud hissing and strong odor.", "Nearby buildings need immediate evacuation."),
            ("Traffic Light Malfunction", "All signals at major intersection stuck on green simultaneously, causing near-misses.", "Heavy commuter traffic backing up."),
            ("Livestock on Highway", "Herd of cows broke through a fence and is wandering onto the motorway.", "Zero visibility due to fog."),

            // FIRE & HAZMAT EMERGENCIES
            ("Structure Fire (Apartment Building)", "Thick black smoke billowing from a third-floor apartment window; flames visible through the glass.", "Multiple residents evacuated to the street; elderly tenant possibly still inside."),
            ("Chimney Fire", "Sparks and flames shooting out of a residential thatched-roof chimney.", "High risk of fire spreading to neighboring historic thatched cottages."),
            ("Vehicle Fire on Motorway", "Passenger car engulfed in flames on the shoulder near the exit ramp.", "Heavy smoke reducing highway visibility; occupants confirmed out of vehicle."),
            ("Barn / Agricultural Fire", "Large hay barn fully involved in fire; loud popping sounds heard from stored fuel drums.", "Livestock being moved away from adjacent stables."),
            ("Boat Fire at Marina", "Fiberglass yacht on fire at the pier; flames threatening adjacent moored vessels.", "Fuel tanks full; risk of explosion and harbor contamination."),
            ("Kitchen Grease Fire", "Cooking oil ignited on a stove and spread to the kitchen cabinets and exhaust hood.", "Caller evacuated with minor burns to hands; smoke filling hallway."),
            ("Forest / Brush Fire", "Dry undergrowth and pine trees burning rapidly near the coastal trail due to strong winds.", "Fire spreading toward nearby summer houses."),
            ("Industrial Warehouse Fire", "Automated fire alarm followed by reports of flames on the roof of a manufacturing plant.", "Hazardous materials and lithium batteries stored on site."),
            ("Electrical Substation Fire", "Loud transformer explosion followed by arcing electrical flames inside a fenced utility station.", "Power outage reported across surrounding residential blocks."),
            ("Garage / Workshop Fire", "Detached residential garage on fire; owner reports acetylene welding tanks inside.", "Neighbors within 100 meters need immediate evacuation."),
            ("Commercial Kitchen / Restaurant Fire", "Fire broke out in the ventilation shaft of a busy downtown restaurant.", "Patrons and staff evacuating onto the pedestrian street."),
            ("Campfire Out of Control", "Unattended beach bonfire spread into dry dune grass and nearby wooden picnic shelters.", "Strong onshore wind pushing embers inland."),

            // NON-EMERGENCY / REJECTABLE CALLS
            ("Noise Complaint (Loud Music)", "Neighbor playing acoustic guitar and singing with the window open at 11:30 PM.", "Caller refuses to knock on neighbor's door first."),
            ("Barking Dog", "Neighbor's golden retriever has been barking at squirrels in the backyard for twenty minutes.", "Dog is inside a fenced yard with water and shelter."),
            ("Illegally Parked Car", "Sedan parked two inches over the white line in a residential parking lot.", "Not blocking traffic, fire hydrants, or driveways."),
            ("Lost Cat", "Caller's indoor cat slipped out the back door 15 minutes ago and is hiding under a bush.", "Caller wants officers sent to help coax it out."),
            ("Fast Food Order Dispute", "Customer furious that the drive-thru forgot their dipping sauce and refuses to refund cash.", "No threats made; purely a civil/customer service dispute."),
            ("Wi-Fi Outage", "Elderly caller demands emergency services fix their router because they cannot watch Netflix.", "No medical or safety emergency present."),
            ("Pothole Report", "Caller hit a minor pothole on a side street and wants to complain about road taxes.", "No vehicle damage or hazard reported."),
            ("Raccoon / Wildlife Sighting", "Caller saw a healthy fox walking through their garden and wants it arrested.", "Animal is not aggressive and is leaving the area."),
            ("Suspiciously Slow Walker", "Resident reports a person wearing a hoodie walking slowly while looking at their phone.", "Individual appears to just be playing a mobile game."),
            ("Expired Parking Meter", "Caller reports a tourist's car has had an expired parking ticket on the dash for 10 minutes.", "Municipal parking enforcement matter, not an emergency."),
            ("Neighbor's Tree Leaves", "Caller upset that wind blew leaves from next door onto their freshly raked lawn.", "Demanding immediate police mediation."),
            ("Tv Remote Broken", "Caller dialed emergency line asking how to reprogram their television remote control.", "Confirmed no actual emergency.")
        };


        // 55 would be enough we can also do it to be asked in the beginning!!!
        public static List<Incident> LoadIncidents(int count = 55)
        {
            var incidents = new List<Incident>();
            DateTime currentCallTime = DateTime.Now;

            var availableTemplates = new List<(string Title, string Description, string DispatchNotes)>(IncidentTemplates);
            ShuffleList(availableTemplates);

            for (int i = 0; i < count; i++)
            {
                // Timestaps for calls decided from 15 secs to 4 mins as to have more realistic call to call time
                int secondsElapsed = _rng.Next(15, 240);
                currentCallTime = currentCallTime.AddSeconds(secondsElapsed);

                // shuffle algorythm
                var template = availableTemplates[i % availableTemplates.Count];
                if (i > 0 && i % availableTemplates.Count == 0)
                {
                    ShuffleList(availableTemplates);
                }

                string location = SonderborgLocations[_rng.Next(SonderborgLocations.Count)];
                string caller = CallerNames[_rng.Next(CallerNames.Count)];
                string phone = GenerateDanishPhoneNumber(caller);

                incidents.Add(new Incident
                {
                    Id = 1001 + i,
                    Title = template.Title,
                    Description = template.Description,
                    Location = location,
                    CallTimestamp = currentCallTime,
                    CallerName = caller,
                    CallerPhone = phone,
                    DispatchNotes = template.DispatchNotes,
                    AssignedPriority = "Unassigned",
                    IsRejected = false
                });
            }

            return incidents;
        }

        private static string GenerateDanishPhoneNumber(string callerName)
        {
            if (callerName == "Anonymous Caller")
                return "Withheld / Hidden";
            if (callerName == "Automated Alarm System")
                return "ALARM-SYS-" + _rng.Next(100, 999);

            // Random calling number generator :D
            return $"+45 {_rng.Next(20, 99)} {_rng.Next(10, 99)} {_rng.Next(10, 99)} {_rng.Next(10, 99)}";
        }

        private static void ShuffleList<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}