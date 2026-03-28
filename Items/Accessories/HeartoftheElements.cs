using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;
using CalamityModClassicPreTrailer.Projectiles.Summon;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class HeartoftheElements : ModItem
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heart of the Elements");
			/* Tooltip.SetDefault("The heart of the world\n" +
            	"Increases max life by 20, life regen by 1, and all damage by 8%\n" +
            	"Increases movement speed by 10% and jump speed by 200%\n" +
            	"Increases damage reduction by 5%\n" +
            	"Increases max mana by 50 and reduces mana usage by 5%\n" +
            	"You grow flowers on the grass beneath you, chance to grow very random dye plants on grassless dirt\n" +
            	"Summons all elementals to protect you\n" +
				"Toggling the visibility of this accessory also toggles the elementals on and off\n" +
				"Stat increases are slightly higher if the elementals are turned off"); */
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
		}
    	
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.defense = 9;
			Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 20;
		}

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (modPlayer.brimstoneWaifu || modPlayer.sandWaifu || modPlayer.sandBoobWaifu || modPlayer.cloudWaifu || modPlayer.sirenWaifu)
            {
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
        	Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, ((float)Main.DiscoR / 255f), ((float)Main.DiscoG / 255f), ((float)Main.DiscoB / 255f));
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.allWaifus = !hideVisual;
            modPlayer.elementalHeart = true;
            if (!hideVisual)
            {
                player.lifeRegen += 1;
                player.statLifeMax2 += 20;
                player.moveSpeed += 0.1f;
                player.jumpSpeedBoost += 2.0f;
                player.endurance += 0.05f;
                player.statManaMax2 += 50;
                player.manaCost *= 0.95f;
                player.GetDamage(DamageClass.Melee) += 0.08f;
                player.GetDamage(DamageClass.Magic) += 0.08f;
                player.GetDamage(DamageClass.Ranged) += 0.08f;
                CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.08f;
                player.GetDamage(DamageClass.Summon) += 0.08f;
                int damage = NPC.downedMoonlord ? 150 : 90;
                float damageMult = CalamityWorldPreTrailer.downedDoG ? 2f : 1f;
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] > 1 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] > 1 ||
                    player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] > 1 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("SandyWaifu").Type] > 1 ||
                    player.ownedProjectileCounts[Mod.Find<ModProjectile>("CloudyWaifu").Type] > 1)
                {
                    player.ClearBuff(Mod.Find<ModBuff>("HotE").Type);
                }
                if (player.FindBuffIndex(Mod.Find<ModBuff>("HotE").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("HotE").Type, 3600, true);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("BigBustyRose").Type, (int)((float)damage * damageMult * player.GetTotalDamage(DamageClass.Summon).Multiplicative), 2f, Main.myPlayer, 0f, 0f);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SirenLure").Type, (int)((float)damage * damageMult * player.GetTotalDamage(DamageClass.Summon).Multiplicative), 2f, Main.myPlayer, 0f, 0f);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("DrewsSandyWaifu").Type, (int)((float)damage * damageMult * player.GetTotalDamage(DamageClass.Summon).Multiplicative), 2f, Main.myPlayer, 0f, 0f);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SandyWaifu").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SandyWaifu").Type, (int)((float)damage * damageMult * player.GetTotalDamage(DamageClass.Summon).Multiplicative), 2f, Main.myPlayer, 0f, 0f);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("CloudyWaifu").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("CloudyWaifu").Type, (int)((float)damage * damageMult * player.GetTotalDamage(DamageClass.Summon).Multiplicative), 2f, Main.myPlayer, 0f, 0f);
                }
            }
            else
            {
                player.lifeRegen += 2;
                player.statLifeMax2 += 25;
                player.moveSpeed += 0.12f;
                player.jumpSpeedBoost += 2.2f;
                player.endurance += 0.06f;
                player.statManaMax2 += 60;
                player.manaCost *= 0.93f;
                player.GetDamage(DamageClass.Melee) += 0.1f;
                player.GetDamage(DamageClass.Magic) += 0.1f;
                player.GetDamage(DamageClass.Ranged) += 0.1f;
                CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.1f;
                player.GetDamage(DamageClass.Summon) += 0.1f;
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] > 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] > 0 ||
                    player.ownedProjectileCounts[Mod.Find<ModProjectile>("DrewsSandyWaifu").Type] > 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("SandyWaifu").Type] > 0 ||
                    player.ownedProjectileCounts[Mod.Find<ModProjectile>("CloudyWaifu").Type] > 0)
                {
                    player.ClearBuff(Mod.Find<ModBuff>("HotE").Type);
                }
            }
			if (player.velocity.Y == 0f && player.grappling[0] == -1) 
			{
				int num4 = (int)player.Center.X / 16;
				int num5 = (int)(player.position.Y + (float)player.height - 1f) / 16;
				if (Main.tile[num4, num5] == null) 
				{
					Main.tile[num4, num5].ClearEverything();
				}
				if (!Main.tile[num4, num5].HasTile && Main.tile[num4, num5].LiquidAmount == 0 && Main.tile[num4, num5 + 1] != null && WorldGen.SolidTile(num4, num5 + 1)) 
				{
					Main.tile[num4, num5].TileFrameY = 0;
					Main.tile[num4, num5].Get<TileWallWireStateData>().Slope = 0;
					Main.tile[num4, num5].Get<TileWallWireStateData>().IsHalfBlock = false;
					if (Main.tile[num4, num5 + 1].TileType == 0)
					{
						if (Main.rand.Next(1000) == 0) 
						{
							Main.tile[num4, num5].Get<TileWallWireStateData>().HasTile = true;
							Main.tile[num4, num5].TileType = 227;
							Main.tile[num4, num5].TileFrameX = (short)(34 * Main.rand.Next(1, 13));
							while (Main.tile[num4, num5].TileFrameX == 144) 
							{
								Main.tile[num4, num5].TileFrameX = (short)(34 * Main.rand.Next(1, 13));
							}
						}
						if (Main.netMode == 1) 
						{
							NetMessage.SendTileSquare(-1, num4, num5, 1, TileChangeType.None);
						}
					}
					if (Main.tile[num4, num5 + 1].TileType == 2) 
					{
						if (Main.rand.Next(2) == 0) 
						{
							Main.tile[num4, num5].Get<TileWallWireStateData>().HasTile = true;
							Main.tile[num4, num5].TileType = 3;
							Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(6, 11));
							while (Main.tile[num4, num5].TileFrameX == 144) 
							{
								Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(6, 11));
							}
						}
						else 
						{
							Main.tile[num4, num5].Get<TileWallWireStateData>().HasTile = true;
							Main.tile[num4, num5].TileType = 73;
							Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(6, 21));
							while (Main.tile[num4, num5].TileFrameX == 144) 
							{
								Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(6, 21));
							}
						}
						if (Main.netMode == 1) 
						{
							NetMessage.SendTileSquare(-1, num4, num5, 1, TileChangeType.None);
						}
					} 
					else if (Main.tile[num4, num5 + 1].TileType == 109) 
					{
						if (Main.rand.Next(2) == 0) 
						{
							Main.tile[num4, num5].Get<TileWallWireStateData>().HasTile = true;
							Main.tile[num4, num5].TileType = 110;
							Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(4, 7));
							while (Main.tile[num4, num5].TileFrameX == 90) 
							{
								Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(4, 7));
							}
						} 
						else 
						{
							Main.tile[num4, num5].Get<TileWallWireStateData>().HasTile = true;
							Main.tile[num4, num5].TileType = 113;
							Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(2, 8));
							while (Main.tile[num4, num5].TileFrameX == 90) 
							{
								Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(2, 8));
							}
						}
						if (Main.netMode == 1) 
						{
							NetMessage.SendTileSquare(-1, num4, num5, 1, TileChangeType.None);
						}
					} 
					else if (Main.tile[num4, num5 + 1].TileType == 60) 
					{
						Main.tile[num4, num5].Get<TileWallWireStateData>().HasTile = true;
						Main.tile[num4, num5].TileType = 74;
						Main.tile[num4, num5].TileFrameX = (short)(18 * Main.rand.Next(9, 17));
						if (Main.netMode == 1) 
						{
							NetMessage.SendTileSquare(-1, num4, num5, 1, TileChangeType.None);
						}
					}
				}
			}
		}
        
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "WifeinaBottle");
			recipe.AddIngredient(null, "WifeinaBottlewithBoobs");
			recipe.AddIngredient(null, "LureofEnthrallment");
			recipe.AddIngredient(null, "EyeoftheStorm");
			recipe.AddIngredient(null, "RoseStone");
			recipe.AddIngredient(null, "AeroStone");
			recipe.AddIngredient(null, "CryoStone");
			recipe.AddIngredient(null, "ChaosStone");
			recipe.AddIngredient(null, "BloomStone");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
    }
}
