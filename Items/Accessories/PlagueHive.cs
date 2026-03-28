using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class PlagueHive : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Plague Hive");
            /* Tooltip.SetDefault("Summons a damaging plague aura around the player to destroy nearby enemies\n" +
                "Releases bees when damaged\n" +
                "Friendly bees inflict the plague\n" +
                "All of your attacks inflict the plague\n" +
                "Makes you immune to the plague\n" +
                "Projectiles spawn plague seekers on enemy hits\n" +
                "The power of your bees and wasps will rival the Moon Lord himself"); */
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 38;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.expert = true;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ToxicHeart");
            recipe.AddIngredient(null, "AlchemicalFlask");
            recipe.AddIngredient(ItemID.HiveBackpack);
            recipe.AddIngredient(ItemID.HoneyComb);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            player.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
            player.starCloakItem_beeCloakOverrideItem.active = true;
            modPlayer.uberBees = true;
            player.strongBees = true;
            modPlayer.alchFlask = true;
            int plagueCounter = 0;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.1f, 2f, 0.2f);
            int num = Mod.Find<ModBuff>("Plague").Type;
            float num2 = 300f;
            bool flag = plagueCounter % 60 == 0;
            int num3 = 60;
            int random = Main.rand.Next(10);
            if (player.whoAmI == Main.myPlayer)
            {
                if (random == 0)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num] && Vector2.Distance(player.Center, nPC.Center) <= num2)
                        {
                            if (nPC.FindBuffIndex(num) == -1)
                            {
                                nPC.AddBuff(num, 120, false);
                            }
                            if (flag)
                            {
                                nPC.StrikeNPC(nPC.CalculateHitInfo(num3, 0));
                                if (Main.netMode != 0)
                                {
                                    NetMessage.SendData(28, -1, -1, null, l, (float)num3, 0f, 0f, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
            }
            plagueCounter++;
            if (plagueCounter >= 180)
            {
                plagueCounter = 0;
            }
        }
    }
}