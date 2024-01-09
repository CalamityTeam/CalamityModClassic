using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TrueTyrantYharimsUltisword : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("True Tyrant's Ultisword");
			//Tooltip.SetDefault("Contains the essence of a forgotten age\n50% chance to give the player the tyrant's fury buff on enemy hits\nThis buff increases melee damage, speed, and crit chance by 30%");
		}

	public override void SetDefaults()
	{
		Item.width = 84;  //The width of the .png file in pixels divided by 2.
		Item.damage = 270;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 21;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 21;
		Item.useTurn = true;
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 84;  //The height of the .png file in pixels divided by 2.
		Item.value = 5250000;  //Value is calculated in copper coins.
		Item.shoot = Mod.Find<ModProjectile>("BlazingPhantomBlade").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 200);
            }
        }
    }
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	switch (Main.rand.Next(3))
		{
    		case 0: type = Mod.Find<ModProjectile>("BlazingPhantomBlade").Type; break;
    		case 1: type = Mod.Find<ModProjectile>("HyperBlade").Type; break;
    		case 2: type = Mod.Find<ModProjectile>("SunlightBlade").Type; break;
    		default: break;
		}
       	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, Main.myPlayer);
    	return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TyrantYharimsUltisword");
		recipe.AddIngredient(null, "CoreofCalamity");
		recipe.AddIngredient(null, "UeliaceBar", 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.RuneWizard);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if (Main.rand.NextBool(2))
    	{
    		player.AddBuff(Mod.Find<ModBuff>("TyrantsFury").Type, 300);
    	}
		target.AddBuff(BuffID.OnFire, 300);
		target.AddBuff(BuffID.Venom, 240);
		target.AddBuff(BuffID.CursedInferno, 180);
		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
		target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 300);
	}
}}
