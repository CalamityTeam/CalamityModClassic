using System.Collections.Generic;
using CalamityModClassicPreTrailer.Items.FabsolStuff;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.TownNPCs
{
    [AutoloadHead]
    public class FAP : ModNPC
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Drunk Princess");

            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 400;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 60;
            NPCID.Sets.AttackAverageChance[NPC.type] = 15;
        }

		public override void SetDefaults()
		{
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 20000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("The Creator")
            });
        }
        
        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active)
                {
                    for (int j = 0; j < player.inventory.Length; j++)
                    {
                        if (player.inventory[j].type == Mod.Find<ModItem>("FabsolsVodka").Type)
                        {
                            return Main.hardMode;
                        }
                    }
                }
            }
            return false;
        }

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
        {
            return new List<string>() {"Cirrus"};
        }

        public override string GetChat()
        {
            if (CalamityWorldPreTrailer.bossRushActive)
                return "We expect you to turn in a perfect performance!";

            if (NPC.homeless)
            {
                if (Main.rand.Next(2) == 0)
                    return "I could smell my vodka from MILES away!";
                else
                    return "Have any spare rooms available? Preferably candle-lit with a hefty supply of booze.";
            }

            if (Main.bloodMoon)
            {
                int random = Main.rand.Next(4);
                if (random == 0)
                {
                    return "Hey! Nice night. I'm gonna make some Bloody Marys. Celery included. Want one?";
                }
                else if (random == 1)
                {
                    return "More blood for the blood gods!";
                }
                else if (random == 2)
                {
                    return "Everyone else is so rude tonight. If they don't get over it soon I'll break down their doors and make them!";
                }
                else
                {
                    Main.player[Main.myPlayer].Hurt(PlayerDeathReason.ByOther(4), Main.player[Main.myPlayer].statLife / 2, -Main.player[Main.myPlayer].direction, false, false, 0, false);
                    return "Being drunk I have no moral compass atm.";
                }
            }

            if (CalamityGlobalNPC.AnyBossNPCS())
                return "Nothard/10, if I fight bosses I wanna feel like screaming 'OH YES DADDY!' while I die repeatedly.";

            IList<string> dialogue = new List<string>();

            if (Main.dayTime)
            {
                dialogue.Add("Like I always say, when you're drunk you can put up with a lot more.");
                dialogue.Add("I'm literally balls drunk off my sass right now.");
                dialogue.Add("I'm either laughing because I'm drunk or because I've lost my mind. Probably both.");
                dialogue.Add("When I'm drunk I'm way happier...at least until the talking worms start to appear.");
                dialogue.Add("I should reprogram the whole game while drunk and send it back to the testers.");
                dialogue.Add("What a great day, might just drink so much that I get poisoned again.");
            }
            else
            {
                dialogue.Add("Now I want to get alcohol, first drinks are on me!");
                dialogue.Add("Here's a challenge...take a drink whenever you take a hit. Oh wait, you'd die.");
                dialogue.Add("Well I was planning to light some candles in order to relax...ah well, time to program while drunk.");
                dialogue.Add("Yes, everyone knows the mechworm is buggy.");
                dialogue.Add("That's west, " + Main.player[Main.myPlayer].name + ". You're fired again.");
                dialogue.Add("Are you sure you're 21? ...alright, fine, but don't tell anyone I sold you this.");
            }
            
            dialogue.Add("Drink something that turns you into a magical flying unicorn so you can be super gay.");
            dialogue.Add("Did anyone ever tell you that large assets cause back pain? Well, they were right.");

            if (BirthdayParty.PartyIsUp)
                dialogue.Add("You'll always find me at parties where booze is involved...well, you'll always find booze where I'm involved.");

            if (Main.invasionType == 4)
                dialogue.Add("Shoot down the space invaders! Sexy time will be postponed if we are invaded by UFOs!");

            if (CalamityWorldPreTrailer.downedCryogen)
                dialogue.Add("God I can't wait to beat on some ice again!");

            if (CalamityWorldPreTrailer.downedLeviathan)
                dialogue.Add("Only things I am attracted to are fish women, women, men who look like women, and that's it.");

            if (NPC.downedMoonlord)
            {
                dialogue.Add("I'll always be watching.");
                dialogue.Add("Why did one creature need that many tentacles? ...actually, don't answer that.");
                if (Main.raining)
                {
                    dialogue.Add("There's chemicals in the water...and it's turning the frogs gay!");
                }
            }

            if (CalamityWorldPreTrailer.downedPolterghast)
                dialogue.Add("I saw a ghost down by the old train tracks once flailing wildly at the lily pads, those were the days.");

            if (CalamityWorldPreTrailer.downedDoG)
                dialogue.Add("I hear it's amazing when the famous purple-stuffed worm out in flap-jaw space with the tuning fork does a raw blink on Hara-kiri rock. I need scissors! 61!");

            int tavernKeep = NPC.FindFirstNPC(NPCID.DD2Bartender);
            if (tavernKeep != -1)
            {
                dialogue.Add("Tell " + Main.npc[tavernKeep].GivenName + " to stop calling me. He�s not wanted.");
                dialogue.Add("My booze will always be better than " + Main.npc[tavernKeep].GivenName + "'s and nobody can convince me otherwise.");
            }

            int dryad = NPC.FindFirstNPC(NPCID.Dryad);
            if (dryad != -1)
                dialogue.Add(Main.npc[dryad].GivenName + " is cool too but she'd outlive me.");

            int permadong = NPC.FindFirstNPC(Mod.Find<ModNPC>("DILF").Type);
            if (permadong != -1)
                dialogue.Add("I never realized how well-endowed " + Main.npc[permadong].GivenName + " was. It had to be the largest icicle I had ever seen.");

            int waifu = NPC.FindFirstNPC(NPCID.Stylist);
            if (waifu != -1)
            {
                dialogue.Add("You still can't stop me from selling alcohol and trying to move in with " + Main.npc[waifu].GivenName + ".");
                dialogue.Add("I love it when " + Main.npc[waifu].GivenName + "'s hands get sticky from all that...wax.");
                dialogue.Add(Main.npc[waifu].GivenName + " works wonders for my hair...among other things.");
            }

            if (Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().chibii)
                dialogue.Add("Is that a toy? Looks like something I'd carry around if I was 5 years old.");

            if ((Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().sirenBoobs && !Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().sirenBoobsHide) ||
                (Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().sirenBoobsAlt && !Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().sirenBoobsAltHide))
                dialogue.Add("Nice...scales...did it get hot in here?");

            if (Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().fabsolVodka)
                dialogue.Add("Oh yeah now you're drinking the good stuff! Do you like it? I created the recipe by mixing fairy dust, crystals, and other magical crap.");

            if (Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().fab)
            {
                dialogue.Add("...so, you're riding me huh? That's not weird at all.");
                dialogue.Add("Are you coming on to me?");
                dialogue.Add("If I was a magical horse in this reality I'd be out in space swirling cocktails as I watch space worms battle for my enjoyment.");
            }

			int donorAmt = CalamityModClassicPreTrailer.donatorList.Count;
			string firstDonor = CalamityModClassicPreTrailer.donatorList[Main.rand.Next(donorAmt)];

			string secondDonor = CalamityModClassicPreTrailer.donatorList[Main.rand.Next(donorAmt)];
			while (secondDonor == firstDonor)
				secondDonor = CalamityModClassicPreTrailer.donatorList[Main.rand.Next(donorAmt)];

			string thirdDonor = CalamityModClassicPreTrailer.donatorList[Main.rand.Next(donorAmt)];
			while (thirdDonor == firstDonor || thirdDonor == secondDonor)
				thirdDonor = CalamityModClassicPreTrailer.donatorList[Main.rand.Next(donorAmt)];

			dialogue.Add("Hey " + firstDonor + ", " + secondDonor + ", and " + thirdDonor + "! You're all pretty good! ...wait, who are you?");

            return dialogue[Main.rand.Next(dialogue.Count)];
        }

		public string Death()
		{
			return "You have failed " + Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().deathCount +
				(Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().deathCount == 1 ? " time." : " times.");
		}

		public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Death Count";
		}

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
			if (firstButton)
			{
                shopName = "Shop";
			}
			else
			{
				Main.npcChatText = Death();
			}
		}
        public static readonly Condition DownedStarGod = CalamityHelper.Create("downedStarGod",() =>  CalamityWorldPreTrailer.downedStarGod);
        public override void AddShops() //charges 50% extra than the original item value
        {
            NPCShop shop = new(Type);
            shop.AddWithCustomValue(ModContent.ItemType<GrapeBeer>(), Item.buyPrice(0, 1, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<RedWine>(), Item.buyPrice(0, 3, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<Moonshine>(), Item.buyPrice(0, 5, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<Vodka>(), Item.buyPrice(0, 5, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<Whiskey>(), Item.buyPrice(0, 5, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<Tequila>(), Item.buyPrice(0, 7, 50, 0))
            .AddWithCustomValue(ModContent.ItemType<Rum>(), Item.buyPrice(0, 7, 50, 0))
            .AddWithCustomValue(ModContent.ItemType<Fireball>(), Item.buyPrice(0, 10, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<Everclear>(), Item.buyPrice(0, 10, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<FabsolsVodka>(), Item.buyPrice(0, 15, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<BloodyMary>(), Item.buyPrice(0, 15, 0, 0), Condition.BloodMoon)
            .AddWithCustomValue(ModContent.ItemType<StarBeamRye>(), Item.buyPrice(0, 20, 0, 0), DownedStarGod ,Condition.TimeNight)
            .AddWithCustomValue(ModContent.ItemType<Screwdriver>(), Item.buyPrice(0, 25, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<MoscowMule>(), Item.buyPrice(0, 25, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<WhiteWine>(), Item.buyPrice(0, 25, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<EvergreenGin>(), Item.buyPrice(0, 25, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<CinnamonRoll>(), Item.buyPrice(0, 25, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<TequilaSunrise>(), Item.buyPrice(0, 30, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<CaribbeanRum>(), Item.buyPrice(0, 30, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<Margarita>(), Item.buyPrice(0, 35, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<BlueCandle>(), Item.buyPrice(2, 0, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<PinkCandle>(), Item.buyPrice(2, 0, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<PurpleCandle>(), Item.buyPrice(2, 0, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<YellowCandle>(), Item.buyPrice(2, 0, 0, 0))
            .AddWithCustomValue(ModContent.ItemType<OddMushroom>(), Item.buyPrice(3, 0, 0, 0))
            .Register();
		}

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 15;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 180;
            randExtraCooldown = 60;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = Mod.Find<ModProjectile>("FabRay").Type;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 2f;
        }
    }
}
