using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SolsticeClaymore : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 74;  //The width of the .png file in pixels divided by 2.
		Item.damage = 245;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 16;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 16;  //Ranges from 1 to 55.
		Item.useTurn = true;
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 74;  //The height of the .png file in pixels divided by 2.
		Item.value = 2400000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Red;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("SolsticeBeam").Type;
		Item.shootSpeed = 16f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.BeamSword);
		recipe.AddIngredient(null, "AstralBar", 20);
		recipe.AddIngredient(ItemID.FragmentSolar, 5);
		recipe.AddIngredient(ItemID.FragmentVortex, 5);
		recipe.AddIngredient(ItemID.FragmentStardust, 5);
		recipe.AddIngredient(ItemID.FragmentNebula, 5);
		recipe.AddIngredient(ItemID.LunarBar, 5);
        recipe.AddTile(TileID.LunarCraftingStation);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
    	int dustType = Main.dayTime ? 
    	Utils.SelectRandom<int>(Main.rand, new int[]
		{
			6,
			259,
			158
		}) : 
    	Utils.SelectRandom<int>(Main.rand, new int[]
		{
			173,
			27,
			234
		});
        if (Main.rand.NextBool(4))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, dustType);
        	Main.dust[dust].noGravity = true;
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if (Main.dayTime)
    	{
    		target.AddBuff(BuffID.Daybreak, 300);
    	}
    	else
    	{
    		target.AddBuff(Mod.Find<ModBuff>("Nightwither").Type, 300);
    	}
	}
}}
