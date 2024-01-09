using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Items.Weapons {
public class TyrantYharimsUltisword : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/TyrantYharimsUltisword");
        return true;
    }


	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Tyrant Yharim's Ultisword");
		Item.width = 25;  //The width of the .png file in pixels divided by 2.
		Item.damage = 92;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 35;
		Item.useStyle = 1;
		Item.useTime = 35;
		Item.knockBack = 5.50f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 28;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		//AddTooltip("Necrotic blade of Jungle King Yharim");
		//AddTooltip2("Launches blazing phantom blades");
		Item.value = 1250000;  //Value is calculated in copper coins.
		Item.rare = 9;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("BlazingPhantomBlade").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "TrueCausticEdge");
		recipe.AddIngredient(ItemID.TrueNightsEdge, 1);
		recipe.AddIngredient(ItemID.FlaskofVenom, 10);
		recipe.AddIngredient(ItemID.FlaskofCursedFlames, 10);
		recipe.AddIngredient(ItemID.HellstoneBar, 15);
		recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 106);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 300);
		target.AddBuff(BuffID.Venom, 240);
		target.AddBuff(BuffID.CursedInferno, 180);
	}
}}
