using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ElementalShortsword : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/ElementalShortsword");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Elemental Shiv");
		Item.useStyle = 3;
		Item.useTurn = false;
		Item.useAnimation = 10;
		Item.useTime = 10;  //Ranges from 1 to 55.
		Item.width = 40;  //The width of the .png file in pixels divided by 2.
		Item.height = 40;  //The height of the .png file in pixels divided by 2.
		Item.damage = 215;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.knockBack = 8.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.useTurn = true;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.maxStack = 1;
		////Tooltip.SetDefault("Don't underestimate the power of shivs");
		Item.shoot = Mod.Find<ModProjectile>("ElementBallShiv").Type;
		Item.shootSpeed = 14f;
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TerraShiv");
		recipe.AddIngredient(null, "GalacticaSingularity", 5);
		recipe.AddIngredient(ItemID.LunarBar, 8);
        recipe.AddTile(TileID.MythrilAnvil);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
		{
			int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, (float)(player.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
			Main.dust[num250].velocity *= 0.2f;
			Main.dust[num250].noGravity = true;
		}
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 500);
    	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 500);
    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 500);
    	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 500);
	}
}}
