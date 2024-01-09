using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class FeralthornClaymore : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/Weapons/FeralthornClaymore");
        return true;
    }


	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Feralthorn Claymore");
		Item.width = 62;  //The width of the .png file in pixels divided by 2.
		Item.damage = 63;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.useTime = 30;
		Item.knockBack = 7.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item8;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 62;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 355000;  //Value is calculated in copper coins.
		Item.rare = 6;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("JungleThorn").Type;
		Item.shootSpeed = 16f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "DraedonBar", 12);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(4) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 44);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Venom, 200);
	}
}}
